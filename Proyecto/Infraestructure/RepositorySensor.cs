using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Tools;

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
            ValidarPosicion(entity);
            base.Add(entity);
        }

        public override void Update(Sensor entity)
        {
            ValidarZona(entity);
            ValidarPosicion(entity);
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
            ZonaVerde? zona = RepositoryZona.GetInstance().GetById(sensor.ZonaId);
            if (zona == null)
            {
                throw new DomainExceptions($"No existe la zona {sensor.ZonaId}.");
            }

            // Un sensor desactivado se puede guardar aunque su zona ya no sirva.
            if (!sensor.Activo)
            {
                return;
            }

            if (!zona.Activo)
            {
                throw new DomainExceptions($"La zona {zona.Nombre} está desactivada.");
            }

            if (sensor.x < 0 || sensor.y < 0 || sensor.x > zona.AnchoM || sensor.y > zona.LargoM)
            {
                throw new DomainExceptions(
                    $"El sensor queda fuera de la zona ({zona.AnchoM} x {zona.LargoM} m).");
            }
        }

        // Dos sensores activos de una zona deben estar separados al menos esta distancia.
        private const double DistanciaMinimaM = 1.0;

        private void ValidarPosicion(Sensor sensor)
        {
            if (!sensor.Activo)
            {
                return;
            }

            bool muyCerca = GetByZona(sensor.ZonaId).Any(s =>
                s.Id != sensor.Id
                && s.Activo
                && Distancia(s, sensor) < DistanciaMinimaM);

            if (muyCerca)
            {
                throw new DomainExceptions(
                    $"Debe haber al menos {DistanciaMinimaM} m entre dos sensores activos de la zona.");
            }
        }

        // Teorema de Pitágoras: los dos catetos son las diferencias en X y en Y.
        private static double Distancia(Sensor a, Sensor b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

    }
}
