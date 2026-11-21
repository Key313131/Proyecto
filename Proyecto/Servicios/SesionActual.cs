using Proyecto.Domain;

namespace Proyecto.Servicios
{
    // Quién inició sesión. Es estático porque en todo el programa hay una sola sesión a la vez:
    // la ventana de inicio la llena y la ventana principal la consulta.
    internal static class SesionActual
    {
        public static Usuario? Usuario { get; private set; }

        public static bool EsAdmin => Usuario?.EsAdmin == true;

        public static void Iniciar(Usuario usuario)
        {
            Usuario = usuario;
        }

        public static void Cerrar()
        {
            Usuario = null;
        }
    }
}
