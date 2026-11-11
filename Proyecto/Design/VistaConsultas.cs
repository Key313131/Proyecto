using Proyecto.Algorithms;
using Proyecto.Domain;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;

namespace Proyecto.Design
{
    // Pantalla de consultas: busca y ordena lecturas con nuestros algoritmos.
    //
    // El trabajo pesado (ordenar, buscar) se hace con Task.Run en OTRO hilo, así la ventana
    // y el simulador siguen funcionando mientras tanto. El hilo de la interfaz solo:
    //   1. lee los filtros y copia las lecturas a un arreglo (antes de Task.Run),
    //   2. muestra el resultado (después del await).
    public partial class VistaConsultas : UserControl
    {
        private const long TodosLosSensores = 0;

        // Lo que se muestra en la tabla. La tabla está en modo virtual: no guarda filas,
        // le pide a CellValueNeeded el valor de cada celda visible. Así puede mostrar
        // 100 000 lecturas sin crear 100 000 filas.
        private Lectura[] _resultado = Array.Empty<Lectura>();

        public VistaConsultas()
        {
            InitializeComponent();

            // Carga lecturas.csv ya: si está dañado, Principal lo avisa al abrir la vista.
            RepositoryLectura lecturas = RepositoryLectura.GetInstance();

            Tema.Tabla(dgvResultados);
            dgvResultados.VirtualMode = true;
            dgvResultados.Columns.Add(Tema.Columna("Id", "Id", 80));
            dgvResultados.Columns.Add(Tema.Columna("Sensor", "Sensor", 70));
            dgvResultados.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvResultados.Columns.Add(Tema.Columna("Tipo", "Tipo", 130));
            dgvResultados.Columns.Add(Tema.Columna("Valor", "Valor", 110));
            dgvResultados.Columns.Add(Tema.Columna("Fecha", "Fecha simulada", 150));

            var opciones = RepositorySensor.GetInstance().GetAll()
                .Select(s => new { s.Id, Texto = $"{s.Id} - {s.Tipo} (zona {s.ZonaId})" })
                .ToList();
            opciones.Insert(0, new { Id = TodosLosSensores, Texto = "Todos los sensores" });
            cmbSensor.DataSource = opciones;

            cmbCriterio.DataSource = Enum.GetValues<CriterioOrden>();
            cmbAlgoritmo.DataSource = Enum.GetValues<Algoritmo>();

            // Por defecto, el último día simulado.
            DateTime hasta = lecturas.UltimaFecha() ?? DateTime.Now;
            dtpHasta.Value = hasta;
            dtpDesde.Value = hasta.AddDays(-1);
            chkFechas_CheckedChanged(this, EventArgs.Empty);

            lstDetalle.Items.Add("Complejidad de los algoritmos:");
            foreach (Algoritmo algoritmo in Enum.GetValues<Algoritmo>())
            {
                string estable = Ordenamiento.EsEstable(algoritmo) ? "estable" : "no estable";
                lstDetalle.Items.Add($"   {algoritmo}: {Ordenamiento.BigO(algoritmo)}, {estable}");
            }
            lstDetalle.Items.Add("   Búsqueda lineal O(n), búsqueda binaria O(log n) (requiere datos ordenados)");

            lblEstado.Text = $"{lecturas.GetAll().Count():N0} lecturas guardadas";
        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {
            dtpDesde.Enabled = chkFechas.Checked;
            dtpHasta.Enabled = chkFechas.Checked;
        }

        // "async void" solo se usa en eventos de controles: el botón no puede esperar a nadie.
        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            if (chkFechas.Checked && dtpDesde.Value > dtpHasta.Value)
            {
                UiHelper.Aviso("La fecha \"desde\" debe ser anterior a la fecha \"hasta\".");
                return;
            }

            FiltroConsulta filtro = new FiltroConsulta
            {
                SensorId = cmbSensor.SelectedValue is long id && id != TodosLosSensores ? id : null,
                Desde = chkFechas.Checked ? dtpDesde.Value : null,
                Hasta = chkFechas.Checked ? dtpHasta.Value : null,
                Criterio = (CriterioOrden)cmbCriterio.SelectedItem!,
                Algoritmo = (Algoritmo)cmbAlgoritmo.SelectedItem!,
                Descendente = chkDescendente.Checked
            };

            Lectura[] copia = CopiarLecturas();
            await Ejecutar("Consultando...", () => ConsultaLecturas.Ejecutar(copia, filtro));
        }

        private async void btnBuscarId_Click(object sender, EventArgs e)
        {
            long id = (long)nudId.Value;
            Lectura[] copia = CopiarLecturas();
            await Ejecutar("Buscando...", () => ConsultaLecturas.BuscarPorId(copia, id));
        }

        private async void btnComparar_Click(object sender, EventArgs e)
        {
            int n = (int)nudN.Value;
            lstDetalle.Items.Clear();
            Ocupado(true, $"Comparando algoritmos con n = {n:N0}... (la ventana sigue respondiendo)");

            // Progress<T> se crea aquí, en el hilo de la interfaz. Cuando el otro hilo llama
            // a Report(...), el texto llega de vuelta a ESTE hilo y se puede tocar la lista.
            Progress<string> progreso = new Progress<string>(linea =>
            {
                if (!IsDisposed)
                {
                    lstDetalle.Items.Add(linea);
                }
            });

            try
            {
                await Task.Run(() => ConsultaLecturas.Comparar(n, progreso));
                if (IsDisposed)
                {
                    return;
                }
                lstDetalle.Items.Add($"Interfaz: hilo #{Environment.CurrentManagedThreadId}");
                Ocupado(false, $"Comparación terminada con n = {n:N0}");
            }
            catch (Exception ex)
            {
                Fallo(ex);
            }
        }

        // La copia se hace aquí, en el hilo de la interfaz, que es el mismo del simulador.
        // Por eso nunca se copia "a medias". Después el otro hilo solo usa esta copia.
        private static Lectura[] CopiarLecturas()
        {
            return RepositoryLectura.GetInstance().GetAll().ToArray();
        }

        // Corre "trabajo" en otro hilo y, al terminar, muestra el resultado.
        private async Task Ejecutar(string mensaje, Func<ResultadoConsulta> trabajo)
        {
            Ocupado(true, mensaje);
            try
            {
                // Task.Run manda "trabajo" a un hilo del ThreadPool. El await espera sin
                // bloquear y, al terminar, el código de abajo sigue en el hilo de la interfaz.
                ResultadoConsulta r = await Task.Run(trabajo);

                // Si mientras tanto el usuario cambió de pantalla, esta vista ya no existe.
                if (IsDisposed)
                {
                    return;
                }
                Mostrar(r);
            }
            catch (Exception ex)
            {
                Fallo(ex);
            }
        }

        private void Mostrar(ResultadoConsulta r)
        {
            _resultado = r.Lecturas;
            dgvResultados.RowCount = 0;             // borra lo anterior
            dgvResultados.RowCount = _resultado.Length;
            dgvResultados.Invalidate();

            lstDetalle.Items.Clear();
            foreach (string linea in r.Detalle)
            {
                lstDetalle.Items.Add(linea);
            }
            lstDetalle.Items.Add($"Calculado en el hilo #{r.Hilo}; la interfaz usa el hilo #{Environment.CurrentManagedThreadId}");

            Ocupado(false, $"{_resultado.Length:N0} lecturas en {r.Milisegundos:0.##} ms");
        }

        private void Fallo(Exception ex)
        {
            if (IsDisposed)
            {
                return;
            }
            Ocupado(false, "La consulta falló");
            UiHelper.Error($"No se pudo completar la operación.\n\n{ex.Message}");
        }

        // Mientras se trabaja se apagan los botones para no lanzar dos tareas a la vez.
        private void Ocupado(bool ocupado, string mensaje)
        {
            btnConsultar.Enabled = !ocupado;
            btnBuscarId.Enabled = !ocupado;
            btnComparar.Enabled = !ocupado;
            Cursor = ocupado ? Cursors.AppStarting : Cursors.Default;
            lblEstado.Text = mensaje;

            foreach (Button boton in new[] { btnConsultar, btnBuscarId, btnComparar })
            {
                boton.BackColor = boton.Enabled ? Tema.Azul : Tema.Linea;
            }
        }

        // La tabla pide el valor de una celda que va a pintar.
        private void dgvResultados_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _resultado.Length)
            {
                return;
            }

            Lectura l = _resultado[e.RowIndex];
            Sensor? sensor = RepositorySensor.GetInstance().GetById(l.SensorId);

            e.Value = dgvResultados.Columns[e.ColumnIndex].Name switch
            {
                "colId" => l.Id,
                "colSensor" => l.SensorId,
                "colZona" => NombreZona(sensor),
                "colTipo" => sensor?.Tipo.ToString() ?? "?",
                "colValor" => $"{l.Valor:0.##} {sensor?.Unidad}",
                "colFecha" => l.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                _ => null
            };
        }

        private static string NombreZona(Sensor? sensor)
        {
            if (sensor == null)
            {
                return "?";
            }
            ZonaVerde? zona = RepositoryZona.GetInstance().GetById(sensor.ZonaId);
            return zona == null ? "?" : $"{zona.Id} - {zona.Nombre}";
        }
    }
}
