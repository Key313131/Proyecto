using System.Security.Cryptography;

namespace Proyecto.Tools
{
    // Las contraseñas NUNCA se guardan como texto. Se guarda un "hash": el resultado de pasar
    // la contraseña por una función que no se puede invertir. Para iniciar sesión se calcula
    // el hash de lo que escribió el usuario y se compara con el guardado.
    //
    // - Sal: bytes al azar distintos para cada usuario. Así dos personas con la misma
    //   contraseña tienen hashes diferentes, y no sirven tablas de hashes ya calculados.
    // - PBKDF2 con SHA-256 repite el cálculo 100.000 veces: para el usuario tarda una fracción
    //   de segundo, pero probar millones de contraseñas se vuelve muy lento.
    internal static class Contrasenas
    {
        private const int BytesSal = 16;
        private const int BytesHash = 32;
        private const int Iteraciones = 100_000;

        public static (byte[] hash, byte[] sal) Crear(string contrasena)
        {
            byte[] sal = RandomNumberGenerator.GetBytes(BytesSal);
            return (Calcular(contrasena, sal), sal);
        }

        public static bool Verificar(string contrasena, byte[] hashGuardado, byte[] sal)
        {
            byte[] hash = Calcular(contrasena, sal);

            // Compara en tiempo constante: no revela cuántos bytes coincidieron.
            return CryptographicOperations.FixedTimeEquals(hash, hashGuardado);
        }

        private static byte[] Calcular(string contrasena, byte[] sal)
        {
            return Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, BytesHash);
        }
    }
}
