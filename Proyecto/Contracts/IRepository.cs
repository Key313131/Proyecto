using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Contracts
{
    internal interface IRepository<T,E> where T : class, IEntity<E>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(E id);
        T?   GetById(E id);
        bool Exists(E id);
        IEnumerable<T> GetAll();



    }
}
