using Proyecto.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
