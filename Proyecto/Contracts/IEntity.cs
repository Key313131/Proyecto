using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Contracts
{
    internal interface IEntity<TID>
    {
        TID Id { get; }
    }
}
