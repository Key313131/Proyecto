namespace Proyecto.Contracts
{
    internal interface IFactory<T>
    {
        static abstract T Create();
        static abstract T Create(string text);

    }
}
