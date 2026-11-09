using Proyecto.DynamicStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Contracts
{
    internal interface IDataStore<T> where T : class
    {
        Lista<T> LoadAll();
        void SaveAll(IEnumerable<T> list);

    }
}
