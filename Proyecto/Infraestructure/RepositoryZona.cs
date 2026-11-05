using Proyecto.Domain;
using Proyecto.Exceptions;
using System;
using System.Collections.Generic;
using Proyecto.Tools;
using System.Text;

namespace Proyecto.Infraestructure
{
    internal class RepositoryZona : RepositoryEntidad<ZonaVerde>
    {
        #region Singleton
        private static RepositoryZona? instance;

        public static RepositoryZona GetInstance()
        {
            if (instance == null)
            {
                instance = new RepositoryZona();
            }
            return instance;
        }
        #endregion

        private RepositoryZona() : base(Configuration.ArchivoZonas) { }

        public override void Delete(long id)
        {
            if (RepositorySensor.GetInstance().GetByZona(id).Any())
            {
                throw new DomainExceptions("No se puede eliminar una zona que tiene sensores.");
            }
            base.Delete(id);
        }
    }
}
