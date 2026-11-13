using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Tools;

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

        // Fecha de la lectura más reciente, o null si no hay ninguna.
        // O(n): revisa todas, porque no se puede suponer que el archivo esté en orden.
        public DateTime? UltimaFecha()
        {
            DateTime? ultima = null;
            foreach (Lectura lectura in GetAll())
            {
                if (ultima == null || lectura.FechaHora > ultima)
                {
                    ultima = lectura.FechaHora;
                }
            }
            return ultima;
        }

        // La lectura más reciente de un sensor, o null si no tiene. O(n).
        public Lectura? UltimaDe(long sensorId)
        {
            Lectura? ultima = null;
            foreach (Lectura lectura in GetAll())
            {
                if (lectura.SensorId == sensorId && (ultima == null || lectura.FechaHora > ultima.FechaHora))
                {
                    ultima = lectura;
                }
            }
            return ultima;
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
