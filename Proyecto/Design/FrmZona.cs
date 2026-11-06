using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;
using System.ComponentModel;

namespace Proyecto.Design
{
    // Formulario emergente: sirve para agregar (sin zona) y para actualizar (con zona).
    public partial class FrmZona : Form
    {
        private readonly RepositoryZona _repo;
        private readonly ZonaVerde _datos;

        // null significa que se está creando una zona nueva.
        private long? _idActual;

        // La vista lo lee al cerrar: si sigue en null, no se guardó nada.
        public long? IdGuardado { get; private set; }

        public FrmZona()
        {
            InitializeComponent();
            _repo = RepositoryZona.GetInstance();
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
            txtId.Text = (_idActual ?? _repo.SiguienteId()).ToString();
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
                long id = _idActual ?? _repo.SiguienteId();

                // Se crea un objeto nuevo en vez de modificar el de la lista. Si un dato
                // es inválido, Create lanza la excepción y la lista queda sin tocar.
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

                if (_idActual == null)
                {
                    _repo.Add(zona);
                }
                else
                {
                    _repo.Update(zona);
                }

                // Antes de Commit: si escribir el archivo falla, la zona ya está en la
                // lista y volver a pulsar Guardar no debe crearla por segunda vez.
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

        // Validating se dispara al salir del campo. No se usa e.Cancel para no dejar
        // al usuario atrapado en el TextBox.
        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            string error = string.IsNullOrWhiteSpace(txtNombre.Text) ? "El nombre es obligatorio" : "";
            errorProvider1.SetError(txtNombre, error);
        }
    }
}
