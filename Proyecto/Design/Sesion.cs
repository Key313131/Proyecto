using Proyecto.Design;

namespace Proyecto
{
    public partial class Sesion : Form
    {
        public Sesion()
        {
            InitializeComponent();
        }

        // Todavía no se verifica el usuario: el proyecto no tiene usuarios guardados.
        private void button1_Click(object sender, EventArgs e)
        {
            Principal principal = new Principal();

            // Sesion es el formulario de Application.Run: si solo se oculta, el
            // programa seguiría vivo después de cerrar Principal.
            principal.FormClosed += (s, a) => Close();

            principal.Show();
            Hide();
        }
    }
}
