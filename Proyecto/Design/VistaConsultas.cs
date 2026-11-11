using Proyecto.Algorithms;
using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto.Design
{
    // Pantalla de consultas: filtra y ordena el historial de lecturas.
    //
    // Para el usuario siempre se ordena con MergeSort (estable y O(n log n) siempre), y
    // solo si hace falta: si las lecturas ya están en el orden pedido no se ordena nada.
    // La comparación entre algoritmos (lo que pide Estructura de Datos) está aparte,
    // en el botón "Análisis de algoritmos".
    //
    // El trabajo pesado lo hace el servicio en OTRO hilo (Task.Run); aquí solo se espera
    // con await y se muestra el resultado.
    public partial class VistaConsultas : UserControl
    {
        private const long TodosLosSensores = 0;

        private readonly ServicioLecturas _lecturas;
        private readonly ServicioSensores _sensores;
        private readonly ServicioZonas _zonas;

        // La tabla está en modo virtual: no guarda filas, le pide a CellValueNeeded el valor
        // de cada celda visible. Así puede mostrar 100 000 lecturas sin crear 100 000 filas.
        private Lectura[] _resultado = Array.Empty<Lectura>();

        // Lo que hizo la última consulta, paso a paso, para la ventana de análisis.
        private Lista<string> _ultimoDetalle = new Lista<string>();

        public VistaConsultas()
        {
            InitializeComponent();
            _lecturas = new ServicioLecturas();
            _sensores = new ServicioSensores();
            _zonas = new ServicioZonas();

            Tema.Tabla(dgvResultados);
            dgvResultados.VirtualMode = true;
            dgvResultados.Columns.Add(Tema.Columna("Id", "Id", 80));
            dgvResultados.Columns.Add(Tema.Columna("Sensor", "Sensor", 70));
            dgvResultados.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvResultados.Columns.Add(Tema.Columna("Tipo", "Tipo", 130));
            dgvResultados.Columns.Add(Tema.Columna("Valor", "Valor", 110));
            dgvResultados.Columns.Add(Tema.Columna("Fecha", "Fecha simulada", 150));

            var opciones = _sensores.Todos()
                .Select(s => new { s.Id, Texto = $"{s.Id} - {s.Tipo} (zona {s.ZonaId})" })
                .ToList();
            opciones.Insert(0, new { Id = TodosLosSensores, Texto = "Todos los sensores" });
            cmbSensor.DataSource = opciones;

            cmbCriterio.DataSource = Enum.GetValues<CriterioOrden>();

            // Por defecto, el último día simulado.
            DateTime hasta = _lecturas.UltimaFecha() ?? DateTime.Now;
            dtpHasta.Value = hasta;
            dtpDesde.Value = hasta.AddDays(-1);
            chkFechas_CheckedChanged(this, EventArgs.Empty);

            lblEstado.Text = $"{_lecturas.Total():N0} lecturas guardadas";
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
                Algoritmo = Algoritmo.MergeSort,
                Descendente = chkDescendente.Checked
            };

            await Ejecutar("Consultando...", () => _lecturas.ConsultarAsync(filtro));
        }

        private async void btnBuscarId_Click(object sender, EventArgs e)
        {
            long id = (long)nudId.Value;
            await Ejecutar("Buscando...", () => _lecturas.BuscarPorIdAsync(id));
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            using FrmAnalisisAlgoritmos analisis = new FrmAnalisisAlgoritmos(_ultimoDetalle);
            analisis.ShowDialog(this);
        }

        // Pide el trabajo al servicio y, cuando termina, muestra el resultado.
        private async Task Ejecutar(string mensaje, Func<Task<ResultadoConsulta>> trabajo)
        {
            Ocupado(true, mensaje);
            try
            {
                // El await espera sin bloquear; el código de abajo sigue en el hilo de la interfaz.
                ResultadoConsulta r = await trabajo();

                // Si mientras tanto el usuario cambió de pantalla, esta vista ya no existe.
                if (IsDisposed)
                {
                    return;
                }
                Mostrar(r);
            }
            catch (Exception ex)
            {
                if (IsDisposed)
                {
                    return;
                }
                Ocupado(false, "La consulta falló");
                UiHelper.Error($"No se pudo completar la consulta.\n\n{ex.Message}");
            }
        }

        private void Mostrar(ResultadoConsulta r)
        {
            _resultado = r.Lecturas;
            dgvResultados.RowCount = 0;             // borra lo anterior
            dgvResultados.RowCount = _resultado.Length;
            dgvResultados.Invalidate();

            _ultimoDetalle = r.Detalle;
            _ultimoDetalle.Add($"Calculado en el hilo #{r.Hilo}; la interfaz usa el hilo #{Environment.CurrentManagedThreadId}");

            Ocupado(false, $"{_resultado.Length:N0} lecturas ({r.Milisegundos:0.#} ms)");
        }

        // Mientras se trabaja se apagan los botones para no lanzar dos consultas a la vez.
        private void Ocupado(bool ocupado, string mensaje)
        {
            btnConsultar.Enabled = !ocupado;
            btnBuscarId.Enabled = !ocupado;
            Cursor = ocupado ? Cursors.AppStarting : Cursors.Default;
            lblEstado.Text = mensaje;

            foreach (Button boton in new[] { btnConsultar, btnBuscarId })
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
            Sensor? sensor = _sensores.Buscar(l.SensorId);

            e.Value = dgvResultados.Columns[e.ColumnIndex].Name switch
            {
                "colId" => l.Id,
                "colSensor" => l.SensorId,
                "colZona" => sensor == null ? "?" : $"{sensor.ZonaId} - {_zonas.Nombre(sensor.ZonaId)}",
                "colTipo" => sensor?.Tipo.ToString() ?? "?",
                "colValor" => $"{l.Valor:0.##} {sensor?.Unidad}",
                "colFecha" => l.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                _ => null
            };
        }
    }
}
