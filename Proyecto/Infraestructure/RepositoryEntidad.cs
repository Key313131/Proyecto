using Proyecto.Contracts;

namespace Proyecto.Infraestructure
{
    internal class RepositoryEntidad<T> : Repository<T, long>
        where T : class, IFactory<T>, ISerialize<T>, IEntity<long>
    {
        public RepositoryEntidad(string path) : base(path) { }

        public long SiguienteId()
        {
            return GetAll().Select(e => e.Id).DefaultIfEmpty(0).Max() + 1;
        }
    }
}
