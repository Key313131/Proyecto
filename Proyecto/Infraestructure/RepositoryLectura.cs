using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Infraestructure
{
    internal class RepositoryLectura : RepositoryEntidad<Lectura>
    {
        #region Singleton
        private static RepositoryLectura? instance;

        public static RepositoryLectura GetInstance()
        {
            if (instance == null)
            {
                instance = new RepositoryLectura();
            }
            return instance;
        }
        #endregion

        private RepositoryLectura() : base(Configuration.ArchivoLecturas) { }

        public IEnumerable<Lectura> GetBySensor(long sensorId)
        {
            return GetAll().Where(l => l.SensorId == sensorId);
        }

        public override void Add(Lectura entity)
        {
            ValidarSensor(entity);
            base.Add(entity);
        }

        public override void Update(Lectura entity)
        {
            ValidarSensor(entity);
            base.Update(entity);
        }

        // Llave forAnea: la lectura debe apuntar a un sensor que exista.
        private static void ValidarSensor(Lectura lectura)
        {
            if (!RepositorySensor.GetInstance().Exists(lectura.SensorId))
            {
                throw new DomainExceptions($"No existe el sensor {lectura.SensorId}.");
            }
        }
    }
}
