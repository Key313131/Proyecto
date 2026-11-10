using Microsoft.VisualBasic;
using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;

namespace Proyecto.Design
{
    public partial class VistaSensores : UserControl
    {
        private readonly RepositorySensor _repo;
        private readonly RepositoryZona _zonas;
        private bool _cargando;

        public VistaSensores()
        {
            InitializeComponent();
            _repo = RepositorySensor.GetInstance();
            _zonas = RepositoryZona.GetInstance();
        }

        private void VistaSensores_Load(object sender, EventArgs e)
        {
            _cargando = true;

            cmbZona.DataSource = _zonas.GetAll()
                .Select(z => new { z.Id, Texto = $"{z.Id} - {z.Nombre}{(z.Activo ? "" : " (inactiva)")}" })
                .ToList();

            _cargando = false;

            //ZonaVerde? primera = _zonas.GetAll().FirstOrDefault();
            //long? id = primera == null ? null : primera.Id;

            Mostrar(_zonas.GetAll().FirstOrDefault()?.Id, null);
        }

        private void Mostrar(long? zonaId, long? sensorId)
        {
            //ternario con un patrón
            ZonaVerde? zona = zonaId is long id ? _zonas.GetById(id) : null;

            List<Sensor> sensores = zona == null ? new List<Sensor>() : _repo.GetByZona(zona.Id).ToList();

            //operador de fusión nula ??
            Sensor? sensor = sensores.FirstOrDefault(s => s.Id == sensorId) ?? sensores.FirstOrDefault();

            _cargando = true;
            if (zona == null)
            {
                cmbZona.SelectedIndex = -1;
            }
            else
            {
                cmbZona.SelectedValue = zona.Id;
            }

            cmbSensor.DataSource = sensores
                .Select(s => new { s.Id, Texto = $"{s.Id} - {s.Tipo}{(s.Activo ? "" : " (inactivo)")}" })
                .ToList();

            if (sensor == null)
            {
                cmbSensor.SelectedIndex = -1;
                nudId.Value = 0;
            }
            else
            {
                cmbSensor.SelectedValue = sensor.Id;
                nudId.Value = sensor.Id;
            }
            _cargando = false;

            lienzo.Mostrar(zona, sensores, sensor?.Id);
            ActualizarBotones(sensor);
        }

        private void ActualizarBotones(Sensor? sensor)
        {
            bool hayZonas = _zonas.GetAll().Any(z => z.Activo);
            btnAgregar.Enabled = hayZonas;
            btnActualizar.Enabled = sensor != null;
            btnDesactivar.Enabled = sensor != null && sensor.Activo;
            lblEstado.Text = hayZonas ? "Listo" : "Primero cree una zona verde";
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando && cmbZona.SelectedValue is long id)
            {
                Mostrar(id, null);
            }
        }

        private void cmbSensor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando && cmbSensor.SelectedValue is long id && cmbZona.SelectedValue is long zona)
            {
                Mostrar(zona, id);
            }
        }

        // escribir un id lleva a ese sensor, aunque este en otra zona
        private void nudId_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando)
            {
                return;
            }

            long id = (long)nudId.Value;
            Sensor? sensor = _repo.GetById(id);
            if (sensor == null)
            {
                ActualizarBotones(null);
                lblEstado.Text = $"No existe el sensor {id}";
                return;
            }
            Mostrar(sensor.ZonaId, sensor.Id);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //conversión con as

            using FrmSensor formulario = new FrmSensor(cmbZona.SelectedValue as long?);
            formulario.ShowDialog(this);
            Refrescar(formulario.IdGuardado, "guardado");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Sensor? sensor = _repo.GetById((long)nudId.Value);
            if (sensor == null)
            {
                return;
            }

            using FrmSensor formulario = new FrmSensor(sensor);
            formulario.ShowDialog(this);
            Refrescar(formulario.IdGuardado, "actualizado");
        }

        private void Refrescar(long? id, string accion)
        {
            if (id is long guardado)
            {
                Mostrar(_repo.GetById(guardado)?.ZonaId, guardado);
                lblEstado.Text = $"Sensor {guardado} {accion}";
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            Sensor? sensor = _repo.GetById((long)nudId.Value);
            if (sensor == null)
            {
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                $"¿Desactivar el sensor {sensor.Id}? Su Id queda reservado.", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _repo.Update(sensor.ConEstado(false));
                Mostrar(sensor.ZonaId, sensor.Id);

                _repo.Commit();
                lblEstado.Text = $"Sensor {sensor.Id} desactivado";
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

        private void btnListar_Click(object sender, EventArgs e)
        {
            using FrmListaSensores lista = new FrmListaSensores(cmbZona.SelectedValue as long?);
            lista.ShowDialog(this);
        }
    }
}
