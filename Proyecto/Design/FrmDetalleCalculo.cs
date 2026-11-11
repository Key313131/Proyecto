namespace Proyecto.Design
{
    // Ventana técnica (para Cálculo Multivariable): de dónde sale cada número del plan.
    public partial class FrmDetalleCalculo : Form
    {
        public FrmDetalleCalculo(IEnumerable<string> lineas)
        {
            InitializeComponent();
            foreach (string linea in lineas)
            {
                lstDetalle.Items.Add(linea);
            }
        }
    }
}
