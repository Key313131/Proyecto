using Proyecto.Infraestructure;

namespace Proyecto.Design
{
    // Listado de solo lectura de todas las zonas, activas e inactivas.
    public partial class FrmListaZonas : Form
    {
        public FrmListaZonas()
        {
            InitializeComponent();

            Tema.Tabla(dgvZonas);
            dgvZonas.Columns.Add(Tema.Columna("Id", "Id", 55));
            dgvZonas.Columns.Add(Tema.Columna("Nombre", "Nombre", 0));
            dgvZonas.Columns.Add(Tema.Columna("Vegetacion", "Vegetación", 110));
            dgvZonas.Columns.Add(Tema.Columna("AnchoM", "Ancho (m)", 90));
            dgvZonas.Columns.Add(Tema.Columna("LargoM", "Largo (m)", 90));
            dgvZonas.Columns.Add(Tema.Columna("AreaM2", "Área (m²)", 95));
            dgvZonas.Columns.Add(Tema.Columna("Kc", "Kc", 60));
            dgvZonas.Columns.Add(Tema.Columna("HumedadMinima", "Hum. mín (%)", 105));
            dgvZonas.Columns.Add(Tema.ColumnaActivo());

            dgvZonas.DataSource = RepositoryZona.GetInstance().GetAll().ToList();
        }
    }
}
