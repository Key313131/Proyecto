using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;

namespace Proyecto.Design
{

    public partial class FrmSensor : Form
    {
        private readonly RepositorySensor _repo;
        private readonly RepositoryZona _zonas;
        private readonly Sensor _datos;
        private long? _idActual;

        public long? IdGuardado { get; private set; }

        public FrmSensor()
        {
            InitializeComponent();
            _repo = RepositorySensor.GetInstance();
            _zonas = RepositoryZona.GetInstance();
            _datos = Sensor.Create();
        }

        public FrmSensor(long? zonaSugerida) : this()
        {
            if (zonaSugerida is long zona)
            {
                _datos.ZonaId = zona;
            }
        }

        internal FrmSensor(Sensor sensor) : this()
        {
            _datos = sensor;
            _idActual = sensor.Id;
        }

        private void FrmSensor_Load(object sender, EventArgs e)
        {
            cmbTipo.DataSource = Enum.GetValues<TipoSensor>();

            cmbZona.DataSource = _zonas.GetAll()
                .Where(z => z.Activo || (_idActual != null && z.Id == _datos.ZonaId))
                .ToList();

            Text = _idActual == null ? "Agregar sensor" : "Actualizar sensor";
            txtId.Text = (_idActual ?? _repo.SiguienteId()).ToString();
            cmbZona.SelectedValue = _datos.ZonaId;
            cmbTipo.SelectedItem = _datos.Tipo;
            UiHelper.Poner(nudX, _datos.x);
            UiHelper.Poner(nudY, _datos.y);
            chkActivo.Checked = _datos.Activo;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbZona.SelectedValue is not long zonaId)
            {
                UiHelper.Aviso("Seleccione una zona verde.");
                return;
            }

            try
            {
                long id = _idActual ?? _repo.SiguienteId();

                Sensor sensor = Sensor.Create(
                    id,
                    zonaId,
                    (TipoSensor)cmbTipo.SelectedItem!,
                    (double)nudX.Value,
                    (double)nudY.Value,
                    chkActivo.Checked);

                if (_idActual == null)
                {
                    _repo.Add(sensor);
                }
                else
                {
                    _repo.Update(sensor);
                }

                _idActual = id;
                IdGuardado = id;

                _repo.Commit();
                DialogResult = DialogResult.OK;
            }
            catch (DomainExceptions ex)
            {
                UiHelper.Aviso(ex.Message);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                UiHelper.Error($"No se pudo escribir el archivo. ¿Está abierto en Excel?\n\n{ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
