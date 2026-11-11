using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;

namespace Proyecto.Servicios
{
    internal class ServicioSensores
    {
        private readonly RepositorySensor _sensores;
        private readonly RepositoryZona _zonas;

        public ServicioSensores()
        {
            _sensores = RepositorySensor.GetInstance();
            _zonas = RepositoryZona.GetInstance();
        }

        public IEnumerable<Sensor> Todos()
        {
            return _sensores.GetAll();
        }

        public IEnumerable<Sensor> DeZona(long zonaId)
        {
            return _sensores.GetByZona(zonaId);
        }

        public IEnumerable<Sensor> ActivosDeZona(long zonaId, TipoSensor tipo)
        {
            return _sensores.GetByZona(zonaId).Where(s => s.Activo && s.Tipo == tipo);
        }

        public Sensor? Buscar(long id)
        {
            return _sensores.GetById(id);
        }

        public long SiguienteId()
        {
            return _sensores.SiguienteId();
        }

        // ¿Hay algo que el simulador pueda medir? Un sensor activo en una zona activa.
        public bool HaySimulables()
        {
            return _sensores.GetAll().Any(s => s.Activo && _zonas.GetById(s.ZonaId)?.Activo == true);
        }

        // Igual que en zonas: crea si el Id no existe, actualiza si ya existe.
        public void Guardar(Sensor sensor)
        {
            if (_sensores.Exists(sensor.Id))
            {
                _sensores.Update(sensor);
            }
            else
            {
                _sensores.Add(sensor);
            }
            _sensores.Commit();
        }

        public Sensor Desactivar(long id)
        {
            Sensor sensor = _sensores.GetById(id) ?? throw new DomainExceptions($"No existe el sensor {id}.");
            Sensor inactivo = sensor.ConEstado(false);
            _sensores.Update(inactivo);
            _sensores.Commit();
            return inactivo;
        }
    }
}
