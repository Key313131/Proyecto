using Proyecto.Design;
using Proyecto.Exceptions;
using Proyecto.Presentacion;
using Proyecto.Servicios;

namespace Proyecto
{
    // Ventana de inicio de sesión.
    //
    // La primera vez que se abre el programa no hay usuarios: la misma ventana sirve para
    // crear la cuenta del administrador. Desde ahí, pide usuario y contraseña para entrar.
    public partial class Sesion : Form
    {
        private readonly ServicioUsuarios? _usuarios;
        private bool _primerInicio;

        public Sesion()
        {
            InitializeComponent();

            try
            {
                _usuarios = new ServicioUsuarios();
            }
            catch (Exception ex) when (ex is DatosCorruptosException or IOException or UnauthorizedAccessException)
            {
                // Sin el archivo de usuarios no se puede entrar; se avisa y se bloquea el botón.
                btnIngresar.Enabled = false;
                Shown += (s, a) => UiHelper.Error($"No se pudo leer el archivo de usuarios.\n\n{ex.Message}");
                return;
            }

            MostrarModo();
        }

        private void MostrarModo()
        {
            _primerInicio = !_usuarios!.HayUsuarios();

            lblTitulo.Text = _primerInicio ? "Crea la cuenta de administrador" : "Inicia sesión en EcoFacility";
            btnIngresar.Text = _primerInicio ? "Crear cuenta" : "Ingresar";

            // El título cambia de largo: se vuelve a centrar sobre el panel.
            lblTitulo.Left = panelLogin.Left + (panelLogin.Width - lblTitulo.Width) / 2;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (nombre.Length == 0 || contrasena.Length == 0)
            {
                UiHelper.Aviso("Escriba el usuario y la contraseña.");
                return;
            }

            try
            {
                if (_primerInicio)
                {
                    _usuarios!.CrearAdministrador(nombre, contrasena);
                    MessageBox.Show($"Se creó la cuenta de administrador \"{nombre}\".\nGuarde bien la contraseña: con ella se registran los demás usuarios.",
                        "Cuenta creada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _usuarios!.IniciarSesion(nombre, contrasena);
                }
            }
            catch (DomainExceptions ex)
            {
                UiHelper.Aviso(ex.Message);
                txtContrasena.Clear();
                txtContrasena.Focus();
                return;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                UiHelper.Error($"No se pudo guardar el archivo de usuarios.\n\n{ex.Message}");
                return;
            }

            AbrirPrincipal();
        }

        private void AbrirPrincipal()
        {
            Principal principal = new Principal();
            principal.FormClosed += (s, a) => Close();
            principal.Show();
            Hide();
        }

        // El ojo muestra u oculta la contraseña.
        private void picVerContrasena_Click(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !txtContrasena.UseSystemPasswordChar;
        }
    }
}
