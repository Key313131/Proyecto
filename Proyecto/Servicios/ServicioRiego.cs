using Proyecto.Calculo;
using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Infraestructure;
using Proyecto.Simulation;

namespace Proyecto.Servicios
{
    // Junta lo medido por los sensores y se lo pasa al planificador (matemática pura).
    internal class ServicioRiego
    {
        private readonly RepositoryZona _zonas;
        private readonly RepositorySensor _sensores;
        private readonly RepositoryLectura _lecturas;

        public ServicioRiego()
        {
            _zonas = RepositoryZona.GetInstance();
            _sensores = RepositorySensor.GetInstance();
            _lecturas = RepositoryLectura.GetInstance();
        }

        public bool HayZonasActivas()
        {
            return _zonas.GetAll().Any(z => z.Activo);
        }

        public PlanRiego Planificar(ParametrosRiego parametros)
        {
            return PlanificadorRiego.Calcular(Entradas(), parametros);
        }

        // Cuánto costaría darle a cada zona todo lo que necesita (sirve de presupuesto sugerido).
        public double CostoCompleto(ParametrosRiego parametros)
        {
            ParametrosRiego sinLimite = new ParametrosRiego
            {
                Temperatura = parametros.Temperatura,
                HorasSol = parametros.HorasSol,
                Tarifa = parametros.Tarifa,
                Presupuesto = 1e15,
                HorasDisponibles = 1e6
            };
            return Planificar(sinLimite).CostoTotal;
        }

        // Temperatura de hoy: el promedio de la última lectura de cada sensor de temperatura activo.
        public double? TemperaturaActual()
        {
            Lista<Lectura> ultimas = new Lista<Lectura>();
            foreach (ZonaVerde zona in _zonas.GetAll().Where(z => z.Activo))
            {
                foreach (Sensor s in Activos(zona, TipoSensor.Temperatura))
                {
                    Lectura? l = _lecturas.UltimaDe(s.Id);
                    if (l != null)
                    {
                        ultimas.Add(l);
                    }
                }
            }
            return ultimas.Count == 0 ? null : ultimas.Average(l => l.Valor);
        }

        // Lo medido en cada zona activa.
        private Lista<EntradaZona> Entradas()
        {
            SimuladorSensores simulador = SimuladorSensores.GetInstance();
            Lista<EntradaZona> entradas = new Lista<EntradaZona>();

            foreach (ZonaVerde zona in _zonas.GetAll().Where(z => z.Activo))
            {
                // Humedad: promedio de la última lectura de cada sensor de humedad activo.
                Lista<Lectura> humedades = Ultimas(zona, TipoSensor.HumedadSuelo);
                double? humedad = humedades.Count > 0 ? humedades.Average(l => l.Valor) : simulador.HumedadDe(zona.Id);

                // Lluvia: la última lectura del pluviómetro de la zona.
                bool lloviendo = Ultimas(zona, TipoSensor.Lluvia).Any(l => l.Valor > 0);

                entradas.Add(new EntradaZona
                {
                    Zona = zona,
                    Humedad = humedad ?? zona.HumedadMinima,
                    Lloviendo = lloviendo,
                    SinLecturas = humedad == null
                });
            }
            return entradas;
        }

        private Lista<Lectura> Ultimas(ZonaVerde zona, TipoSensor tipo)
        {
            Lista<Lectura> ultimas = new Lista<Lectura>();
            foreach (Sensor s in Activos(zona, tipo))
            {
                Lectura? l = _lecturas.UltimaDe(s.Id);
                if (l != null)
                {
                    ultimas.Add(l);
                }
            }
            return ultimas;
        }

        private IEnumerable<Sensor> Activos(ZonaVerde zona, TipoSensor tipo)
        {
            return _sensores.GetByZona(zona.Id).Where(s => s.Activo && s.Tipo == tipo);
        }
    }
}
