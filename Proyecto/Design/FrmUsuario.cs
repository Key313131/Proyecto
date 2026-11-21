using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto.Design
{
    // Formulario para que el administrador registre un usuario nuevo.
    // Los usuarios registrados aquí pueden entrar al programa, pero no registrar a otros.
    public partial class FrmUsuario : Form
    {
        private readonly ServicioUsuarios _usuarios;

        public FrmUsuario()
        {
            InitializeComponent();
            _usuarios = new ServicioUsuarios();
            lblAyuda.Text = $"La contraseña debe tener al menos {Usuario.LargoMinimoContrasena} caracteres.";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtContrasena.Text != txtConfirmar.Text)
            {
                UiHelper.Aviso("Las contraseñas no coinciden.");
                txtConfirmar.Clear();
                txtConfirmar.Focus();
                return;
            }

            try
            {
                Usuario nuevo = _usuarios.Registrar(txtUsuario.Text, txtContrasena.Text);
                MessageBox.Show($"Se registró el usuario \"{nuevo.Nombre}\".", "Usuario registrado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
