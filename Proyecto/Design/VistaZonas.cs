using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto.Design
{
    public partial class VistaZonas : UserControl
    {
        private readonly ServicioZonas _zonas;
        private readonly ServicioSensores _sensores;

        // evita que los eventos de los controles reaccionen a cambios hechos por código.
        private bool _cargando;

        public VistaZonas()
        {
            InitializeComponent();
            _zonas = new ServicioZonas();
            _sensores = new ServicioSensores();
        }

        private void VistaZonas_Load(object sender, EventArgs e)
        {
            CargarZonas(_zonas.Todas().FirstOrDefault());
        }

        private void CargarZonas(ZonaVerde? seleccionar)
        {
            _cargando = true;
            cmbZona.DataSource = _zonas.Todas()
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

            lienzo.Mostrar(zona, zona == null ? new List<Sensor>() : _sensores.DeZona(zona.Id));
            btnActualizar.Enabled = zona != null;
            btnDesactivar.Enabled = zona != null && zona.Activo;
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando && cmbZona.SelectedValue is long id)
            {
                Mostrar(_zonas.Buscar(id));
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
            ZonaVerde? zona = _zonas.Buscar(id);
            Mostrar(zona);
            lblEstado.Text = zona != null ? "Listo" : $"No existe la zona {id}";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using FrmZona formulario = new FrmZona();
            formulario.ShowDialog(this);
            Refrescar(formulario.IdGuardado, "guardada");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ZonaVerde? zona = _zonas.Buscar((long)nudId.Value);
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
                CargarZonas(_zonas.Buscar(guardada));
                lblEstado.Text = $"Zona {guardada} {accion}";
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            ZonaVerde? zona = _zonas.Buscar((long)nudId.Value);
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
                ZonaVerde inactiva = _zonas.Desactivar(zona.Id);
                CargarZonas(inactiva);
                lblEstado.Text = $"Zona {zona.Id} desactivada";
            }
            catch (DomainExceptions ex)
            {
                UiHelper.Aviso(ex.Message);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // El cambio ya quedó en memoria: se muestra, aunque el archivo no se escribió.
                CargarZonas(_zonas.Buscar(zona.Id));
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
