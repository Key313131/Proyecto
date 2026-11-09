using Proyecto.Design;

namespace Proyecto
{
    public partial class Sesion : Form
    {
        public Sesion()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            Principal principal = new Principal();

            principal.FormClosed += (s, a) => Close();

            principal.Show();
            Hide();
        }
    }
}
