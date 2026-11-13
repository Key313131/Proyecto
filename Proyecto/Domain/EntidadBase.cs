using Proyecto.Contracts;
using Proyecto.Exceptions;

namespace Proyecto.Domain
{
    internal class EntidadBase : IEntity<long>
    {
        private long _id;

        public long Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new DomainExceptions("El Id debe ser un número mayor que 0.");
                _id = value;
            }

        }
    }
}
