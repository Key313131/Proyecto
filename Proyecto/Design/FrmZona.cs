using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Proyecto.Presentacion
{
    public partial class FrmZona : Form
    {
        private readonly RepositoryZona _repo;

        // null significa que se está creando una zona nueva.
        private long? _idActual;

        // Evita que SelectionChanged reemplace los campos mientras se recarga la tabla.
        private bool _cargando;

        public FrmZona()
        {
            InitializeComponent();
            _repo = RepositoryZona.GetInstance();
        }

        private void FrmZona_Load(object sender, EventArgs e)
        {
            cmbVegetacion.DataSource = Enum.GetValues<TipoVegetacion>();

            ZonaVerde? primera = _repo.GetAll().FirstOrDefault();
            if (primera == null)
            {
                Nuevo();
                return;
            }

            CargarTabla(primera.Id);
            MostrarExistente(primera);
        }

        private void CargarTabla(long? seleccionar)
        {
            _cargando = true;
            dgvZonas.DataSource = _repo.GetAll().ToList();
            UiHelper.SeleccionarFila(dgvZonas, colId, seleccionar);
            _cargando = false;
        }

        private void dgvZonas_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando)
            {
                return;
            }

            if (UiHelper.IdSeleccionado(dgvZonas, colId) is long id)
            {
                ZonaVerde? zona = _repo.GetById(id);
                if (zona != null)
                {
                    MostrarExistente(zona);
                }
            }
        }

        private void Nuevo()
        {
            _idActual = null;
            CargarTabla(null);

            // ZonaVerde.Create() trae los valores por defecto del dominio.
            MostrarEnCampos(ZonaVerde.Create());
            txtId.Text = _repo.SiguienteId().ToString();
            txtNombre.Clear();
            btnEliminar.Enabled = false;
            lblEstado.Text = "Nueva zona";
            ActiveControl = txtNombre;
        }

        private void MostrarExistente(ZonaVerde zona)
        {
            _idActual = zona.Id;
            MostrarEnCampos(zona);
            txtId.Text = zona.Id.ToString();
            btnEliminar.Enabled = true;
            lblEstado.Text = "Listo";
        }

        private void MostrarEnCampos(ZonaVerde zona)
        {
            errorProvider1.Clear();
            txtNombre.Text = zona.Nombre;
            cmbVegetacion.SelectedItem = zona.Vegetacion;
            UiHelper.Poner(nudArea, zona.AreaM2);
            UiHelper.Poner(nudX, zona.X);
            UiHelper.Poner(nudY, zona.Y);
            UiHelper.Poner(nudKc, zona.Kc);
            UiHelper.Poner(nudHumedad, zona.HumedadMinima);
            UiHelper.Poner(nudRaiz, zona.ProfundidadRaizM);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
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
                    (double)nudArea.Value,
                    (double)nudX.Value,
                    (double)nudY.Value,
                    (double)nudKc.Value,
                    (double)nudHumedad.Value,
                    (double)nudRaiz.Value);

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
                CargarTabla(id);
                MostrarExistente(zona);

                _repo.Commit();
                lblEstado.Text = $"Zona {id} guardada";
            }
            catch (DomainExceptions ex)
            {
                UiHelper.Aviso(ex.Message);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                lblEstado.Text = "Cambios sin guardar en el archivo";
                UiHelper.Error($"No se pudo escribir el archivo. ¿Está abierto en Excel?\n\n{ex.Message}");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idActual is not long id)
            {
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                $"¿Eliminar la zona \"{txtNombre.Text}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _repo.Delete(id);
                Nuevo();
                _repo.Commit();
                lblEstado.Text = $"Zona {id} eliminada";
            }
            catch (DomainExceptions ex)
            {
                UiHelper.Aviso(ex.Message);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException
                                          or DatosCorruptosException)
            {
                lblEstado.Text = "Cambios sin guardar en el archivo";
                UiHelper.Error($"No se pudo acceder a los datos.\n\n{ex.Message}");
            }
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
