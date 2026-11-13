using Proyecto.Domain;
using Proyecto.Exceptions;
using Proyecto.Infraestructure;

namespace Proyecto.Servicios
{
    // CAPA DE SERVICIOS (hace el papel del "controlador" en MVC).
    //
    // Las vistas ya no usan los repositorios: le piden todo a un servicio. Así:
    //   - la lógica (qué se guarda, en qué orden, qué se valida) queda en un solo lugar,
    //   - se puede probar sin abrir ventanas,
    //   - si mañana se cambia el CSV por una base de datos, las vistas no se enteran.
    //
    // Los servicios no guardan datos propios (los datos están en los repositorios),
    // por eso no necesitan ser Singleton: cada vista crea el suyo con "new".
    //
    // Errores: si un dato es inválido lanza DomainExceptions; si el archivo no se puede
    // escribir, sale la IOException. La vista decide cómo mostrarle eso al usuario.
    internal class ServicioZonas
    {
        private readonly RepositoryZona _zonas;

        public ServicioZonas()
        {
            // Cargar aquí hace que un CSV dañado se detecte al crear la vista. Los sensores
            // también se cargan, aunque esta clase no los usa, para avisar del error enseguida.
            _zonas = RepositoryZona.GetInstance();
            RepositorySensor.GetInstance();
        }

        public IEnumerable<ZonaVerde> Todas()
        {
            return _zonas.GetAll();
        }

        public IEnumerable<ZonaVerde> Activas()
        {
            return _zonas.GetAll().Where(z => z.Activo);
        }

        public bool HayActivas()
        {
            return Activas().Any();
        }

        // Zonas que se pueden elegir para un sensor: las activas y, si el sensor ya existe,
        // también la suya aunque esté inactiva (para no perderla al editarlo).
        public IEnumerable<ZonaVerde> ParaSensor(Sensor? sensorExistente)
        {
            return _zonas.GetAll().Where(z => z.Activo || (sensorExistente != null && z.Id == sensorExistente.ZonaId));
        }

        public ZonaVerde? Buscar(long id)
        {
            return _zonas.GetById(id);
        }

        public string Nombre(long id)
        {
            return _zonas.GetById(id)?.Nombre ?? "(sin zona)";
        }

        public long SiguienteId()
        {
            return _zonas.SiguienteId();
        }

        // Crea la zona si su Id no existe; si ya existe, la actualiza.
        // Por eso, si el archivo falló la primera vez y el usuario vuelve a pulsar
        // Guardar, no se crea dos veces: la segunda vez ya existe en memoria y se actualiza.
        public void Guardar(ZonaVerde zona)
        {
            if (_zonas.Exists(zona.Id))
            {
                _zonas.Update(zona);
            }
            else
            {
                _zonas.Add(zona);
            }
            _zonas.Commit();
        }

        // Una zona no se borra: se desactiva. Devuelve la zona ya desactivada.
        public ZonaVerde Desactivar(long id)
        {
            ZonaVerde zona = _zonas.GetById(id) ?? throw new DomainExceptions($"No existe la zona {id}.");
            ZonaVerde inactiva = zona.ConEstado(false);
            _zonas.Update(inactiva);
            _zonas.Commit();
            return inactiva;
        }
    }
}
