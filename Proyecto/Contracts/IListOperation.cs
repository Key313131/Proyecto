namespace Proyecto.Contracts
{
    internal interface IListOperation<T>
    {
        void Add(T data);
        void Delete(int position);
        void Delete(T data);
        T? ElementAt(int position);
        void Set(int index, T data);
    }
}
