using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Presentacion;
using Proyecto.Servicios;
using System.ComponentModel;

namespace Proyecto.Design
{
    public partial class FrmZona : Form
    {
        private readonly ServicioZonas _zonas;
        private readonly ZonaVerde _datos;

        private long? _idActual;

        public long? IdGuardado { get; private set; }

        public FrmZona()
        {
            InitializeComponent();
            _zonas = new ServicioZonas();
            _datos = ZonaVerde.Create();
        }

        internal FrmZona(ZonaVerde zona) : this()
        {
            _datos = zona;
            _idActual = zona.Id;
        }

        private void FrmZona_Load(object sender, EventArgs e)
        {
            cmbVegetacion.DataSource = Enum.GetValues<TipoVegetacion>();

            Text = _idActual == null ? "Agregar zona verde" : "Actualizar zona verde";
            txtId.Text = (_idActual ?? _zonas.SiguienteId()).ToString();
            txtNombre.Text = _idActual == null ? "" : _datos.Nombre;
            cmbVegetacion.SelectedItem = _datos.Vegetacion;
            UiHelper.Poner(nudAncho, _datos.AnchoM);
            UiHelper.Poner(nudLargo, _datos.LargoM);
            UiHelper.Poner(nudX, _datos.X);
            UiHelper.Poner(nudY, _datos.Y);
            UiHelper.Poner(nudKc, _datos.Kc);
            UiHelper.Poner(nudHumedad, _datos.HumedadMinima);
            UiHelper.Poner(nudRaiz, _datos.ProfundidadRaizM);
            chkActivo.Checked = _datos.Activo;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                long id = _idActual ?? _zonas.SiguienteId();

                ZonaVerde zona = ZonaVerde.Create(
                    id,
                    txtNombre.Text,
                    (TipoVegetacion)cmbVegetacion.SelectedItem!,
                    (double)nudAncho.Value,
                    (double)nudLargo.Value,
                    (double)nudX.Value,
                    (double)nudY.Value,
                    (double)nudKc.Value,
                    (double)nudHumedad.Value,
                    (double)nudRaiz.Value,
                    chkActivo.Checked);

                // Se recuerda el Id antes de guardar: si el archivo falla y se reintenta,
                // se sigue usando el mismo Id (el servicio lo actualiza en vez de crearlo otra vez).
                _idActual = id;
                IdGuardado = id;

                _zonas.Guardar(zona);
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


        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            string error = string.IsNullOrWhiteSpace(txtNombre.Text) ? "El nombre es obligatorio" : "";
            errorProvider1.SetError(txtNombre, error);
        }
    }
}
