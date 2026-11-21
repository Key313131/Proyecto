using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;

namespace Proyecto.Servicios
{
    // Controlador de las cuentas.
    //
    // Reglas:
    //  1. Si todavía no hay usuarios, el primero que se crea queda como administrador.
    //  2. Solo el administrador puede registrar más usuarios, y esos nunca son administradores.
    //
    // Errores: un dato inválido o una contraseña incorrecta lanzan DomainExceptions; si el
    // archivo no se puede escribir, sale la IOException. La vista decide cómo mostrarlo.
    internal class ServicioUsuarios
    {
        private readonly RepositoryUsuario _usuarios;

        public ServicioUsuarios()
        {
            // Cargar aquí hace que un usuarios.csv dañado se detecte al abrir la ventana.
            _usuarios = RepositoryUsuario.GetInstance();
        }

        public bool HayUsuarios()
        {
            return _usuarios.GetAll().Any();
        }

        // Primer inicio: crea la cuenta del administrador y abre la sesión con ella.
        public Usuario CrearAdministrador(string nombre, string contrasena)
        {
            if (HayUsuarios())
            {
                throw new DomainExceptions("El administrador ya fue creado. Inicie sesión.");
            }

            Usuario admin = Usuario.Nuevo(_usuarios.SiguienteId(), nombre, contrasena, esAdmin: true);
            Guardar(admin);
            SesionActual.Iniciar(admin);
            return admin;
        }

        public Usuario IniciarSesion(string nombre, string contrasena)
        {
            Usuario? usuario = _usuarios.GetByNombre(nombre);

            // El mismo mensaje en los dos casos: no se le dice a un extraño qué usuarios existen.
            if (usuario == null || !usuario.ContrasenaCorrecta(contrasena))
            {
                throw new DomainExceptions("Usuario o contraseña incorrectos.");
            }

            SesionActual.Iniciar(usuario);
            return usuario;
        }

        // Solo el administrador registra usuarios. La ventana principal ya oculta el botón a los
        // demás; esta revisión evita que se salte la regla desde otro lugar del código.
        public Usuario Registrar(string nombre, string contrasena)
        {
            if (!SesionActual.EsAdmin)
            {
                throw new DomainExceptions("Solo el administrador puede registrar usuarios.");
            }

            Usuario nuevo = Usuario.Nuevo(_usuarios.SiguienteId(), nombre, contrasena, esAdmin: false);
            Guardar(nuevo);
            return nuevo;
        }

        private void Guardar(Usuario usuario)
        {
            _usuarios.Add(usuario);
            try
            {
                _usuarios.Commit();
            }
            catch
            {
                // Si no se pudo escribir el archivo, se quita de memoria para no quedar a medias.
                _usuarios.Delete(usuario.Id);
                throw;
            }
        }
    }
}
