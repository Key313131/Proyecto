using Proyecto.Presentacion;

namespace Proyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Principal p1 = new Principal();
            p1.Show();
        }
    }
}
