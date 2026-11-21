using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Servicios;
using Proyecto.Tools;

namespace Proyecto.Pruebas
{
    // Pruebas del inicio de sesión. Trabajan con el usuarios.csv de la carpeta Datos de las
    // PRUEBAS (bin de Pruebas), nunca con el del programa. Se borra al empezar y al terminar.
    internal static class PruebasUsuarios
    {
        public static void Ejecutar(Action<bool, string> revisar)
        {
            PruebaContrasenas(revisar);

            string archivo = Configuration.ArchivoUsuarios;
            Directory.CreateDirectory(Configuration.Carpeta);
            File.Delete(archivo);
            try
            {
                PruebaReglas(revisar, archivo);
            }
            finally
            {
                SesionActual.Cerrar();
                File.Delete(archivo);
            }
        }

        private static void PruebaContrasenas(Action<bool, string> revisar)
        {
            (byte[] hash1, byte[] sal1) = Contrasenas.Crear("riego2026");
            (byte[] hash2, byte[] sal2) = Contrasenas.Crear("riego2026");

            revisar(Contrasenas.Verificar("riego2026", hash1, sal1), "La contraseña correcta se acepta");
            revisar(!Contrasenas.Verificar("Riego2026", hash1, sal1), "Una contraseña distinta (mayúscula) se rechaza");
            revisar(!hash1.SequenceEqual(hash2), "La misma contraseña da hashes distintos por la sal");

            Usuario u = Usuario.Nuevo(1, "ana", "riego2026", esAdmin: false);
            string linea = Usuario.Serialize(u);
            revisar(!linea.Contains("riego2026"), "La contraseña no aparece en la línea del CSV");

            Usuario leido = Usuario.Deserialize(linea);
            revisar(leido.Nombre == "ana" && leido.ContrasenaCorrecta("riego2026") && !leido.EsAdmin,
                "El usuario se guarda y se lee igual del CSV");

            revisar(Lanza(() => Usuario.Nuevo(2, "ana;x", "riego2026", false)), "Un nombre con \";\" se rechaza");
            revisar(Lanza(() => Usuario.Nuevo(2, "beto", "123", false)), "Una contraseña de menos de 6 caracteres se rechaza");
        }

        private static void PruebaReglas(Action<bool, string> revisar, string archivo)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
            revisar(!servicio.HayUsuarios(), "Al empezar no hay usuarios (primer inicio)");

            Usuario admin = servicio.CrearAdministrador("admin", "clave123");
            revisar(admin.EsAdmin && SesionActual.EsAdmin, "El primer usuario creado es el administrador");
            revisar(File.Exists(archivo) && !File.ReadAllText(archivo).Contains("clave123"),
                "Se guarda en usuarios.csv sin la contraseña en texto");
            revisar(Lanza(() => servicio.CrearAdministrador("otro", "clave123")), "No se puede crear un segundo administrador");

            Usuario ana = servicio.Registrar("ana", "riego2026");
            revisar(!ana.EsAdmin, "El administrador registra un usuario que no es administrador");
            revisar(Lanza(() => servicio.Registrar("ANA", "otraclave")), "No se repite un usuario (sin importar mayúsculas)");

            revisar(Lanza(() => servicio.IniciarSesion("ana", "malaclave")), "Con la contraseña equivocada no entra");
            servicio.IniciarSesion("Ana", "riego2026");
            revisar(SesionActual.Usuario?.Nombre == "ana" && !SesionActual.EsAdmin, "Ana entra y no queda como administradora");
            revisar(Lanza(() => servicio.Registrar("carlos", "clave456")), "Un usuario normal no puede registrar a otros");

            servicio.IniciarSesion("admin", "clave123");
            revisar(SesionActual.EsAdmin, "El administrador vuelve a entrar como administrador");
        }

        private static bool Lanza(Action accion)
        {
            try
            {
                accion();
                return false;
            }
            catch (DomainExceptions)
            {
                return true;
            }
        }
    }
}
