namespace Proyecto.Design
{
    // Ventana técnica: de dónde sale cada número (del plan de riego o del dashboard).
    public partial class FrmDetalleCalculo : Form
    {
        public FrmDetalleCalculo(IEnumerable<string> lineas) : this(lineas, "Cálculo detallado del plan de riego") { }

        public FrmDetalleCalculo(IEnumerable<string> lineas, string titulo)
        {
            InitializeComponent();
            Text = titulo;
            foreach (string linea in lineas)
            {
                lstDetalle.Items.Add(linea);
            }
        }
    }
}
