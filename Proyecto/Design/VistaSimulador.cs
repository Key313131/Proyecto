using Proyecto.Domain;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;
using Proyecto.Simulation;

namespace Proyecto.Design
{
    // Pantalla del simulador: lo inicia o detiene y muestra la última lectura de cada sensor.
    // El simulador sigue corriendo aunque se cambie de pantalla; esta vista solo lo observa.
    public partial class VistaSimulador : UserControl
    {
        private readonly SimuladorSensores _sim;

        public VistaSimulador()
        {
            InitializeComponent();
            _sim = SimuladorSensores.GetInstance();

            // Carga lecturas.csv ya (si está dañado, Principal lo avisa al abrir la vista)
            // y pone el reloj en la hora de la última lectura guardada.
            _sim.Retomar();

            Tema.Tabla(dgvLecturas);
            dgvLecturas.Columns.Add(Tema.Columna("Sensor", "Sensor", 70));
            dgvLecturas.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvLecturas.Columns.Add(Tema.Columna("Tipo", "Tipo", 130));
            dgvLecturas.Columns.Add(Tema.Columna("Valor", "Valor", 120));
            dgvLecturas.Columns.Add(Tema.Columna("Hora", "Hora simulada", 150));

            nudIntervalo.Value = Math.Clamp(_sim.IntervaloMs, (int)nudIntervalo.Minimum, (int)nudIntervalo.Maximum);

            // Suscribirse: cuando el simulador avise, se llaman estos métodos.
            _sim.PasoTerminado += Refrescar;
            _sim.Aviso += AgregarEvento;

            // Al cerrar la vista hay que desuscribirse; si no, el simulador seguiría
            // llamando a métodos de controles que ya no existen.
            Disposed += (s, a) =>
            {
                _sim.PasoTerminado -= Refrescar;
                _sim.Aviso -= AgregarEvento;
            };

            // Los avisos anteriores se muestran del más nuevo al más viejo.
            foreach (string aviso in _sim.Historial)
            {
                lstEventos.Items.Insert(0, aviso);
            }

            Refrescar();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            bool haySensores = RepositorySensor.GetInstance().GetAll().Any(s => s.Activo
                && RepositoryZona.GetInstance().GetById(s.ZonaId)?.Activo == true);
            if (!haySensores)
            {
                UiHelper.Aviso("No hay sensores activos en zonas activas para simular.");
                return;
            }

            _sim.IntervaloMs = (int)nudIntervalo.Value;
            _sim.Iniciar();
            Refrescar();
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            _sim.Detener();
            Refrescar();
        }

        // Cambiar el intervalo funciona incluso con la simulación en marcha.
        private void nudIntervalo_ValueChanged(object sender, EventArgs e)
        {
            _sim.IntervaloMs = (int)nudIntervalo.Value;
        }

        private void Refrescar()
        {
            RepositorySensor sensores = RepositorySensor.GetInstance();
            RepositoryZona zonas = RepositoryZona.GetInstance();

            // Se reutilizan las filas y solo se cambian sus valores: es más liviano que
            // reemplazar el DataSource en cada paso, y la tabla no parpadea.
            int cantidad = _sim.UltimasLecturas.Count();
            if (dgvLecturas.Rows.Count != cantidad)
            {
                dgvLecturas.Rows.Clear();
                for (int i = 0; i < cantidad; i++)
                {
                    dgvLecturas.Rows.Add();
                }
            }

            int fila = 0;
            foreach (Lectura l in _sim.UltimasLecturas)
            {
                Sensor? sensor = sensores.GetById(l.SensorId);
                ZonaVerde? zona = sensor == null ? null : zonas.GetById(sensor.ZonaId);
                string regando = zona != null && _sim.RegandoZona(zona.Id) ? " (regando)" : "";

                dgvLecturas.Rows[fila].SetValues(
                    l.SensorId,
                    zona == null ? "?" : $"{zona.Id} - {zona.Nombre}{regando}",
                    sensor?.Tipo.ToString() ?? "?",
                    $"{l.Valor:0.##} {sensor?.Unidad}",
                    l.FechaHora.ToString("dd/MM HH:mm"));
                fila++;
            }

            lblReloj.Text = $"Hora simulada\n{_sim.Reloj:dd/MM/yyyy HH:mm}";
            lblClima.Text = _sim.Lloviendo ? "Lloviendo" : "Sin lluvia";
            lblTotal.Text = $"Generadas: {_sim.TotalGeneradas}";
            lblCola.Text = $"En cola: {_sim.EnCola}";
            lblEstado.Text = _sim.EnMarcha
                ? $"En marcha: cada {_sim.IntervaloMs} ms avanza {SimuladorSensores.PasoSimulado.TotalMinutes} min"
                : "Detenido";

            btnIniciar.Enabled = !_sim.EnMarcha;
            btnDetener.Enabled = _sim.EnMarcha;

            // Un botón deshabilitado se pinta apagado para que se note que no se puede usar.
            btnIniciar.BackColor = btnIniciar.Enabled ? Color.LimeGreen : Tema.Linea;
            btnDetener.BackColor = btnDetener.Enabled ? Color.FromArgb(214, 69, 69) : Tema.Linea;
        }

        private void AgregarEvento(string texto)
        {
            lstEventos.Items.Insert(0, texto);
            if (lstEventos.Items.Count > SimuladorSensores.MaxHistorial)
            {
                lstEventos.Items.RemoveAt(lstEventos.Items.Count - 1);
            }
        }
    }
}
