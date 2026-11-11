using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto.Design
{

    public partial class FrmSensor : Form
    {
        private readonly ServicioSensores _sensores;
        private readonly ServicioZonas _zonas;
        private readonly Sensor _datos;
        private long? _idActual;

        public long? IdGuardado { get; private set; }

        public FrmSensor()
        {
            InitializeComponent();
            _sensores = new ServicioSensores();
            _zonas = new ServicioZonas();
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

            cmbZona.DataSource = _zonas.ParaSensor(_idActual == null ? null : _datos).ToList();

            Text = _idActual == null ? "Agregar sensor" : "Actualizar sensor";
            txtId.Text = (_idActual ?? _sensores.SiguienteId()).ToString();
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
                long id = _idActual ?? _sensores.SiguienteId();

                Sensor sensor = Sensor.Create(
                    id,
                    zonaId,
                    (TipoSensor)cmbTipo.SelectedItem!,
                    (double)nudX.Value,
                    (double)nudY.Value,
                    chkActivo.Checked);

                _idActual = id;
                IdGuardado = id;

                _sensores.Guardar(sensor);
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
