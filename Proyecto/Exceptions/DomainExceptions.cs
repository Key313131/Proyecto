using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Exceptions
{
    internal class DomainExceptions : Exception
    {
        public DomainExceptions(string mensaje) : base(mensaje) { }
    }
}
