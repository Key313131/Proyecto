using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Infraestructure
{
    internal class RepositorySensor : RepositoryEntidad<Sensor>
    {
        #region Singleton
        private static RepositorySensor? instance;

        public static RepositorySensor GetInstance()
        {
            if (instance == null)
            {
                instance = new RepositorySensor();
            }
            return instance;
        }
        #endregion

        private RepositorySensor() : base(Configuration.ArchivoSensores) { }

        public IEnumerable<Sensor> GetByZona(long zonaId)
        {
            return GetAll().Where(s => s.ZonaId == zonaId);
        }

        public override void Add(Sensor entity)
        {
            ValidarZona(entity);
            base.Add(entity);
        }

        public override void Update(Sensor entity)
        {
            ValidarZona(entity);
            base.Update(entity);
        }

        public override void Delete(long id)
        {
            
            if (RepositoryLectura.GetInstance().GetBySensor(id).Any())
            {
                throw new DomainExceptions(
                    "No se puede eliminar un sensor con lecturas. Desactívelo en su lugar.");
            }
            base.Delete(id);
        }

        // seria como la llave foranea del sensor debe apuntar a una zona que exista.
        private static void ValidarZona(Sensor sensor)
        {
            if (!RepositoryZona.GetInstance().Exists(sensor.ZonaId))
            {
                throw new DomainExceptions($"No existe la zona {sensor.ZonaId}.");
            }
        }
    }
}
