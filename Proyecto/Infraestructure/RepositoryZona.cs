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

        public override void Update(ZonaVerde entity)
        {
            List<Sensor> sensores = RepositorySensor.GetInstance().GetByZona(entity.Id).ToList();

            if (!entity.Activo && sensores.Any(s => s.Activo))
            {
                throw new DomainExceptions("Primero desactive los sensores de esta zona.");
            }

            if (sensores.Any(s => s.x > entity.AnchoM || s.y > entity.LargoM))
            {
                throw new DomainExceptions("Con esas medidas algunos sensores quedarían fuera de la zona.");
            }

            base.Update(entity);
        }

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
