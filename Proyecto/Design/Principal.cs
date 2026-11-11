using Proyecto.Exceptions;
using Proyecto.Simulation;
using Proyecto.Tools;

namespace Proyecto.Design
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            lblDatos.Text = $"Datos en: {Configuration.Carpeta}";

            // Al cerrar, se detiene el simulador para que guarde lo que tenga en la cola.
            FormClosing += (s, a) => SimuladorSensores.GetInstance().Detener();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Mostrar(() => new VistaZonas(), btnZonas);
        }

        private void Mostrar<T>(Func<T> crear, Button boton) where T : UserControl
        {
            if (panelContenido.Controls.OfType<T>().Any())
            {
                return;
            }

            try
            {
                
                T vista = crear();
                vista.Dock = DockStyle.Fill;


                while (panelContenido.Controls.Count > 0)
                {
                    panelContenido.Controls[0].Dispose();
                }

                panelContenido.Controls.Add(vista);
                ActiveControl = vista;
                MarcarBoton(boton);
            }
            catch (Exception ex) when (ex is DatosCorruptosException or IOException
                                          or UnauthorizedAccessException)
            {
                MessageBox.Show($"No se pudieron cargar los datos.\n\n{ex.Message}",
                    "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MarcarBoton(Button activo)
        {
            btnZonas.BackColor = Tema.Tarjeta;
            btnSensores.BackColor = Tema.Tarjeta;
            btnSimulador.BackColor = Tema.Tarjeta;
            btnConsultas.BackColor = Tema.Tarjeta;
            btnCalculo.BackColor = Tema.Tarjeta;
            btnTablero.BackColor = Tema.Tarjeta;
            activo.BackColor = Tema.Azul;
        }

        private void btnZonas_Click(object sender, EventArgs e)
        {
            Mostrar(() => new VistaZonas(), btnZonas);
        }

        private void btnSensores_Click(object sender, EventArgs e)
        {
            Mostrar(() => new VistaSensores(), btnSensores);
        }

        private void btnSimulador_Click(object sender, EventArgs e)
        {
            Mostrar(() => new VistaSimulador(), btnSimulador);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            Mostrar(() => new VistaConsultas(), btnConsultas);
        }

        private void btnCalculo_Click(object sender, EventArgs e)
        {
            Mostrar(() => new VistaCalculo(), btnCalculo);
        }

        private void btnTablero_Click(object sender, EventArgs e)
        {
            Mostrar(() => new VistaTablero(), btnTablero);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
