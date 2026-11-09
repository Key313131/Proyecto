namespace Proyecto.Contracts
{
    // Operaciones básicas que debe ofrecer cualquier lista del proyecto.
    // Es la misma interfaz que hicimos en Estructura de Datos.
    internal interface IListOperation<T>
    {
        void Add(T data);
        void Delete(int position);
        void Delete(T data);
        T? ElementAt(int position);
        void Set(int index, T data);
    }
}
