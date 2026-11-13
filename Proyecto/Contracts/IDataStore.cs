using Proyecto.DynamicStructure;

namespace Proyecto.Contracts
{
    internal interface IDataStore<T> where T : class
    {
        Lista<T> LoadAll();
        void SaveAll(IEnumerable<T> list);

    }
}
