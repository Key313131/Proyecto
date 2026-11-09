using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;
using Proyecto.Presentacion;

namespace Proyecto.Design
{
    public partial class VistaZonas : UserControl
    {
        private readonly RepositoryZona _repo;
        private readonly RepositorySensor _sensores;

        // evita que los eventos de los controles reaccionen a cambios hechos por código.
        private bool _cargando;

        public VistaZonas()
        {
            InitializeComponent();
            _repo = RepositoryZona.GetInstance();
            _sensores = RepositorySensor.GetInstance();
        }

        private void VistaZonas_Load(object sender, EventArgs e)
        {
            CargarZonas(_repo.GetAll().FirstOrDefault());
        }

        private void CargarZonas(ZonaVerde? seleccionar)
        {
            _cargando = true;
            cmbZona.DataSource = _repo.GetAll() //halomolacha
                .Select(z => new { z.Id, Texto = $"{z.Id} - {z.Nombre}{(z.Activo ? "" : " (inactiva)")}" })
                .ToList();
            _cargando = false;

            Mostrar(seleccionar);
        }

        private void Mostrar(ZonaVerde? zona)
        {
            _cargando = true;
            if (zona == null)
            {
                cmbZona.SelectedIndex = -1;
            }
            else
            {
                cmbZona.SelectedValue = zona.Id;
                nudId.Value = zona.Id;
            }
            _cargando = false;

            lienzo.Mostrar(zona, zona == null ? new List<Sensor>() : _sensores.GetByZona(zona.Id));
            btnActualizar.Enabled = zona != null;
            btnDesactivar.Enabled = zona != null && zona.Activo;
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando && cmbZona.SelectedValue is long id)
            {
                Mostrar(_repo.GetById(id));
                lblEstado.Text = "Listo";
            }
        }

        // Escribir un Id tiene el mismo efecto que elegir esa zona en el combo.
        private void nudId_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando)
            {
                return;
            }

            long id = (long)nudId.Value;
            Mostrar(_repo.GetById(id));
            lblEstado.Text = _repo.Exists(id) ? "Listo" : $"No existe la zona {id}";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using FrmZona formulario = new FrmZona();
            formulario.ShowDialog(this);
            Refrescar(formulario.IdGuardado, "guardada");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ZonaVerde? zona = _repo.GetById((long)nudId.Value);
            if (zona == null)
            {
                return;
            }

            using FrmZona formulario = new FrmZona(zona);
            formulario.ShowDialog(this);
            Refrescar(formulario.IdGuardado, "actualizada");
        }

        private void Refrescar(long? id, string accion)
        {
            if (id is long guardada)
            {
                CargarZonas(_repo.GetById(guardada));
                lblEstado.Text = $"Zona {guardada} {accion}";
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            ZonaVerde? zona = _repo.GetById((long)nudId.Value);
            if (zona == null)
            {
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                $"¿Desactivar la zona \"{zona.Nombre}\"? Su Id queda reservado.", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                ZonaVerde inactiva = zona.ConEstado(false);
                _repo.Update(inactiva);
                CargarZonas(inactiva);

                _repo.Commit();
                lblEstado.Text = $"Zona {zona.Id} desactivada";
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
            using FrmListaZonas lista = new FrmListaZonas();
            lista.ShowDialog(this);
        }
    }
}
