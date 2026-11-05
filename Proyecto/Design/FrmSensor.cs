using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto.Presentacion
{
    public partial class FrmSensor : Form
    {
        private readonly RepositorySensor _repo;
        private readonly RepositoryZona _zonas;

        // null significa que se está creando un sensor nuevo.
        private long? _idActual;

        // Evita que SelectionChanged reemplace los campos mientras se recarga la tabla.
        private bool _cargando;

        public FrmSensor()
        {
            InitializeComponent();
            _repo = RepositorySensor.GetInstance();
            _zonas = RepositoryZona.GetInstance();
        }

        private void FrmSensor_Load(object sender, EventArgs e)
        {
            cmbTipo.DataSource = Enum.GetValues<TipoSensor>();
            CargarZonas();

            Sensor? primero = _repo.GetAll().FirstOrDefault();
            if (primero == null)
            {
                Nuevo();
                return;
            }

            CargarTabla(primero.Id);
            MostrarExistente(primero);
        }

        // La ventana de zonas puede cambiar los datos mientras esta sigue abierta, así
        // que al volver a ella se actualizan el combo y los nombres de la tabla.
        private void FrmSensor_Activated(object sender, EventArgs e)
        {
            CargarZonas();
            CargarTabla(_idActual);
        }

        private void CargarZonas()
        {
            object? seleccion = cmbZona.SelectedValue;
            List<ZonaVerde> zonas = _zonas.GetAll().ToList();

            cmbZona.DataSource = zonas;
            if (seleccion != null)
            {
                cmbZona.SelectedValue = seleccion;
            }

            btnGuardar.Enabled = zonas.Count > 0;
            if (zonas.Count == 0)
            {
                lblEstado.Text = "Primero cree una zona verde";
            }
        }

        private void CargarTabla(long? seleccionar)
        {
            _cargando = true;

            // El tipo anónimo agrega columnas que Sensor no tiene, como el nombre de la zona.
            dgvSensores.DataSource = _repo.GetAll()
                .Select(s => new
                {
                    s.Id,
                    Zona = _zonas.GetById(s.ZonaId)?.Nombre ?? "(sin zona)",
                    s.Tipo,
                    s.Unidad,
                    s.x,
                    s.y,
                    s.Activo
                })
                .ToList();

            UiHelper.SeleccionarFila(dgvSensores, colId, seleccionar);
            _cargando = false;
        }

        private void dgvSensores_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando)
            {
                return;
            }

            if (UiHelper.IdSeleccionado(dgvSensores, colId) is long id)
            {
                Sensor? sensor = _repo.GetById(id);
                if (sensor != null)
                {
                    MostrarExistente(sensor);
                }
            }
        }

        private void Nuevo()
        {
            _idActual = null;
            CargarTabla(null);

            MostrarEnCampos(Sensor.Create());
            txtId.Text = _repo.SiguienteId().ToString();
            btnEliminar.Enabled = false;

            // Si el foco cae en la tabla, esta selecciona sola su primera fila.
            ActiveControl = cmbZona;

            if (_zonas.GetAll().Any())
            {
                lblEstado.Text = "Nuevo sensor";
            }
        }

        private void MostrarExistente(Sensor sensor)
        {
            _idActual = sensor.Id;
            MostrarEnCampos(sensor);
            txtId.Text = sensor.Id.ToString();
            btnEliminar.Enabled = true;
            lblEstado.Text = "Listo";
        }

        private void MostrarEnCampos(Sensor sensor)
        {
            cmbZona.SelectedValue = sensor.ZonaId;
            cmbTipo.SelectedItem = sensor.Tipo;
            UiHelper.Poner(nudX, sensor.x);
            UiHelper.Poner(nudY, sensor.y);
            chkActivo.Checked = sensor.Activo;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
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

                // Se crea un objeto nuevo en vez de modificar el de la lista. Si un dato
                // es inválido, Create lanza la excepción y la lista queda sin tocar.
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

                // Antes de Commit: si escribir el archivo falla, el sensor ya está en la
                // lista y volver a pulsar Guardar no debe crearlo por segunda vez.
                CargarTabla(id);
                MostrarExistente(sensor);

                _repo.Commit();
                lblEstado.Text = $"Sensor {id} guardado";
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
                $"¿Eliminar el sensor {id}?", "Confirmar",
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
                lblEstado.Text = $"Sensor {id} eliminado";
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
    }
}
