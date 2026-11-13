namespace Proyecto.Contracts
{
    internal interface IEntity<TID>
    {
        TID Id { get; }
    }
}
