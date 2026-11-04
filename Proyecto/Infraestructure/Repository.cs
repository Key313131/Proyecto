using Proyecto.Contracts;
using Proyecto.Exceptions;
using Proyecto.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Infraestructure
{
    internal class Repository<T, E> : IRepository<T, E>, IUnitOfWork
        where T : class, IFactory<T>, ISerialize<T>, IEntity<E>
        where E : notnull
    {
        private readonly IDataStore<T> _store;
        private readonly List<T> _cache;

        public Repository(string path)
        {
            _store = new DataStore<T>(path);
            _cache = _store.LoadAll().ToList(); //hay que cambiar la listas por las propias
        }

        // hay que habalr sobre esto
        public virtual void Add(T entity)
        {
            if (Exists(entity.Id))
            {
                throw new DomainExceptions($"Ya existe un registro con Id {entity.Id}.");
            }
            _cache.Add(entity);
        }

        public virtual void Update(T entity)
        {
            int index = _cache.FindIndex(d => d.Id.Equals(entity.Id));
            if (index == -1)
            {
                throw new DomainExceptions($"No existe un registro con Id {entity.Id}.");
            }
            _cache[index] = entity;
        }

        public virtual void Delete(E id)
        {
            T? entity = GetById(id);
            if (entity == null)
            {
                throw new DomainExceptions($"No existe un registro con Id {id}.");
            }
            _cache.Remove(entity);
        }

        public T? GetById(E id)
        {
            return _cache.FirstOrDefault(d => d.Id.Equals(id));
        }

        public bool Exists(E id)
        {
            return _cache.Exists(d => d.Id.Equals(id));
        }

        public IEnumerable<T> GetAll()
        {
            return _cache;
        }

        public void Commit()
        {
            _store.SaveAll(_cache);
        }
    }
}
