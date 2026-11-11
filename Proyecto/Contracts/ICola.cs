namespace Proyecto.Contracts
{

    internal interface ICola<T>
    {
        void Enqueue(T data);
        T? Dequeue();
        T? Peek();
        void Clear();
    }
}
