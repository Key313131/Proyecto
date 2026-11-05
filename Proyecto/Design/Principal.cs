using Proyecto.Exceptions;
using Proyecto.Tools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto.Presentacion
{
    // Ventana MDI: contiene a los demás formularios.
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            lblDatos.Text = $"Datos en: {Configuration.Carpeta}";
        }

        // Genérico para no repetir este código por cada formulario. Si la ventana
        // ya está abierta la trae al frente en lugar de abrir una segunda copia.
        private void Abrir<T>(Func<T> crear) where T : Form
        {
            T? abierto = MdiChildren.OfType<T>().FirstOrDefault();
            if (abierto != null)
            {
                abierto.Activate();
                return;
            }

            try
            {
                T formulario = crear();
                formulario.MdiParent = this;
                formulario.Show();
            }
            catch (Exception ex) when (ex is DatosCorruptosException or IOException
                                          or UnauthorizedAccessException)
            {
                // El constructor del formulario carga los archivos.
                MessageBox.Show($"No se pudieron cargar los datos.\n\n{ex.Message}",
                    "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void zonasVerdesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(() => new FrmZona());
        }

        private void sensoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir(() => new FrmSensor());
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void mosaicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
