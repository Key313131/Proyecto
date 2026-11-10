using Proyecto.Domain;
using Proyecto.Infraestructure;

namespace Proyecto.Design
{
    public partial class FrmListaSensores : Form
    {
        private const long TodasLasZonas = 0;

        private readonly RepositorySensor _repo;
        private readonly RepositoryZona _zonas;

        public FrmListaSensores() : this(null) { }

        public FrmListaSensores(long? zonaInicial)
        {
            InitializeComponent();
            _repo = RepositorySensor.GetInstance();
            _zonas = RepositoryZona.GetInstance();

            Tema.Tabla(dgvSensores);
            dgvSensores.Columns.Add(Tema.Columna("Id", "Id", 55));
            dgvSensores.Columns.Add(Tema.Columna("ZonaId", "Id zona", 75));
            dgvSensores.Columns.Add(Tema.Columna("Zona", "Zona", 0));
            dgvSensores.Columns.Add(Tema.Columna("Tipo", "Tipo", 120));
            dgvSensores.Columns.Add(Tema.Columna("Unidad", "Unidad", 70));
            dgvSensores.Columns.Add(Tema.Columna("x", "X", 65));
            dgvSensores.Columns.Add(Tema.Columna("y", "Y", 65));
            dgvSensores.Columns.Add(Tema.ColumnaActivo());

           
            var opciones = _zonas.GetAll()
                .Select(z => new { z.Id, Texto = $"{z.Id} - {z.Nombre}" })
                .ToList();
            opciones.Insert(0, new { Id = TodasLasZonas, Texto = "Todas las zonas" });

            cmbFiltro.DataSource = opciones;
            cmbFiltro.SelectedValue = zonaInicial ?? TodasLasZonas;
        }


        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltro.SelectedValue is not long zonaId)
            {
                return;
            }

            IEnumerable<Sensor> sensores = zonaId == TodasLasZonas ? _repo.GetAll() : _repo.GetByZona(zonaId);

            // polimorfismo con interfaces
            dgvSensores.DataSource = sensores
                .Select(s => new
                {
                    s.Id,
                    s.ZonaId,
                    Zona = _zonas.GetById(s.ZonaId)?.Nombre ?? "(sin zona)",
                    s.Tipo,
                    s.Unidad,
                    s.x,
                    s.y,
                    s.Activo
                })
                .ToList();
        }
    }
}
