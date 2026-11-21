using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Tools;

namespace Proyecto.Infraestructure
{
    internal class RepositoryUsuario : RepositoryEntidad<Usuario>
    {
        #region Singleton
        private static RepositoryUsuario? instance;

        public static RepositoryUsuario GetInstance()
        {
            if (instance == null)
            {
                instance = new RepositoryUsuario();
            }
            return instance;
        }
        #endregion

        private RepositoryUsuario() : base(Configuration.ArchivoUsuarios) { }

        // El nombre se compara sin distinguir mayúsculas: "Ana" y "ana" son el mismo usuario.
        public Usuario? GetByNombre(string nombre)
        {
            string buscado = (nombre ?? "").Trim();
            return GetAll().FirstOrDefault(u => string.Equals(u.Nombre, buscado, StringComparison.OrdinalIgnoreCase));
        }

        public override void Add(Usuario entity)
        {
            if (GetByNombre(entity.Nombre) != null)
            {
                throw new DomainExceptions($"Ya existe el usuario \"{entity.Nombre}\".");
            }
            base.Add(entity);
        }
    }
}
