namespace Proyecto.Contracts
{
    internal interface ISerialize<T>
    {
        static abstract string Serialize(T obj);
        static abstract T Deserialize(string text);

    }
}
