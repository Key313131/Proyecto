using Proyecto.Contracts;
using Proyecto.Exceptions;
using Proyecto.Tools;

namespace Proyecto.Domain
{
    // Una cuenta para entrar al programa. No hay roles: el primer usuario que se crea es el
    // administrador y es el único que puede registrar más usuarios.
    // La contraseña no se guarda: solo su hash y su sal (ver Tools/Contrasenas).
    internal class Usuario : EntidadBase, IFactory<Usuario>, ISerialize<Usuario>
    {
        public const int LargoMinimoContrasena = 6;

        private string _nombre = "";

        public string Nombre
        {
            get => _nombre;
            set
            {
                string nombre = (value ?? "").Trim();
                if (nombre.Length < 3 || nombre.Length > 30)
                {
                    throw new DomainExceptions("El usuario debe tener entre 3 y 30 caracteres.");
                }
                // Solo letras, números, punto, guion y guion bajo. Así nunca lleva el ";" del CSV.
                if (!nombre.All(c => char.IsLetterOrDigit(c) || c is '.' or '-' or '_'))
                {
                    throw new DomainExceptions("El usuario solo puede tener letras, números, punto, guion y guion bajo.");
                }
                _nombre = nombre;
            }
        }

        public byte[] Hash { get; private set; } = Array.Empty<byte>();
        public byte[] Sal { get; private set; } = Array.Empty<byte>();
        public bool EsAdmin { get; private set; }

        private Usuario() { }

        public static Usuario Create()
        {
            return new Usuario();
        }

        public static Usuario Create(string text)
        {
            return Deserialize(text);
        }

        // Crea un usuario nuevo a partir de la contraseña escrita.
        public static Usuario Nuevo(long id, string nombre, string contrasena, bool esAdmin)
        {
            if (string.IsNullOrEmpty(contrasena) || contrasena.Length < LargoMinimoContrasena)
            {
                throw new DomainExceptions($"La contraseña debe tener al menos {LargoMinimoContrasena} caracteres.");
            }

            (byte[] hash, byte[] sal) = Contrasenas.Crear(contrasena);
            return Create(id, nombre, hash, sal, esAdmin);
        }

        // Formato: Id;Nombre;Hash;Sal;EsAdmin   (hash y sal en Base64)
        public static Usuario Create(long id, string nombre, byte[] hash, byte[] sal, bool esAdmin)
        {
            return new Usuario
            {
                Id = id,
                Nombre = nombre,
                Hash = hash,
                Sal = sal,
                EsAdmin = esAdmin
            };
        }

        public bool ContrasenaCorrecta(string contrasena)
        {
            return Contrasenas.Verificar(contrasena, Hash, Sal);
        }

        private const int campos = 5;

        public static string Serialize(Usuario usuario)
        {
            return Csv.Unir(usuario.Id, usuario.Nombre, Convert.ToBase64String(usuario.Hash),
                Convert.ToBase64String(usuario.Sal), usuario.EsAdmin);
        }

        public static Usuario Deserialize(string text)
        {
            string[] partes = Csv.Partir(text, campos);
            try
            {
                return Create(
                    Csv.Long(partes[0]),
                    partes[1],
                    Convert.FromBase64String(partes[2]),
                    Convert.FromBase64String(partes[3]),
                    Csv.Bool(partes[4]));
            }
            catch (Exception ex) when (
                ex is FormatException or OverflowException
                or ArgumentException or DomainExceptions)
            {
                throw new DatosCorruptosException("No se pudo leer el usuario", text, ex);
            }
        }

        public override string ToString()
        {
            return EsAdmin ? $"{Nombre} (administrador)" : Nombre;
        }
    }
}
