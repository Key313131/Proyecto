using Proyecto.Exceptions;
using Proyecto.Tools;

namespace Proyecto.Design
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            lblDatos.Text = $"Datos en: {Configuration.Carpeta}";
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Mostrar(() => new VistaZonas(), btnZonas);
        }

        // Pone una vista en el lado derecho. Recibe una función y no usa new T() porque
        // new T() envuelve las excepciones y el catch de abajo no las reconocería.
        private void Mostrar<T>(Func<T> crear, Button boton) where T : UserControl
        {
            // Si esa vista ya está en pantalla no hace falta crearla otra vez.
            if (panelContenido.Controls.OfType<T>().Any())
            {
                return;
            }

            try
            {
                // El constructor de la vista carga los archivos: puede fallar.
                T vista = crear();
                vista.Dock = DockStyle.Fill;

                // Dispose libera la vista anterior y además la quita del panel.
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
