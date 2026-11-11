using Proyecto.Algorithms;
using Proyecto.Calculo;
using Proyecto.Domain;
using Proyecto.DynamicStructure;
using Proyecto.Infraestructure;

namespace Proyecto.Servicios
{
    public enum PeriodoTablero
    {
        UltimoDia,
        UltimaSemana,
        Todo
    }

    // Junta las lecturas de caudal de cada zona y se las pasa al cálculo del tablero.
    internal class ServicioTablero
    {
        private readonly RepositoryZona _zonas;
        private readonly RepositorySensor _sensores;
        private readonly RepositoryLectura _lecturas;

        public ServicioTablero()
        {
            _zonas = RepositoryZona.GetInstance();
            _sensores = RepositorySensor.GetInstance();
            _lecturas = RepositoryLectura.GetInstance();
        }

        public bool HayLecturas()
        {
            return _lecturas.UltimaFecha() != null;
        }

        public static string Texto(PeriodoTablero periodo)
        {
            return periodo switch
            {
                PeriodoTablero.UltimoDia => "Último día simulado",
                PeriodoTablero.UltimaSemana => "Última semana simulada",
                _ => "Todo el historial"
            };
        }

        // Rango de fechas del periodo, contado hacia atrás desde la última lectura.
        public (DateTime desde, DateTime hasta) Rango(PeriodoTablero periodo)
        {
            DateTime hasta = _lecturas.UltimaFecha() ?? DateTime.Now;
            DateTime primera = PrimeraFecha() ?? hasta;
            DateTime desde = periodo switch
            {
                PeriodoTablero.UltimoDia => hasta.AddDays(-1),
                PeriodoTablero.UltimaSemana => hasta.AddDays(-7),
                _ => primera
            };
            // Si el historial es más corto que el periodo, se empieza en la primera lectura:
            // así el riego tradicional no se cuenta por días en los que no hubo datos.
            return (desde < primera ? primera : desde, hasta);
        }

        public Tablero Calcular(PeriodoTablero periodo, double tarifa, double lamina, double meta,
                                CriterioTablero orden, bool descendente)
        {
            (DateTime desde, DateTime hasta) = Rango(periodo);
            ParametrosTablero p = new ParametrosTablero
            {
                Desde = desde,
                Hasta = hasta,
                Tarifa = tarifa,
                LaminaTradicional = lamina,
                MetaAhorro = meta,
                Orden = orden,
                Descendente = descendente
            };
            return CalculoTablero.Calcular(Entradas(desde, hasta), p);
        }

        // Para cada zona activa: las lecturas de cada medidor de caudal dentro del periodo.
        // Se usan todos sus medidores (aunque hoy estén inactivos): su historial sigue valiendo.
        private Lista<EntradaConsumo> Entradas(DateTime desde, DateTime hasta)
        {
            Lista<EntradaConsumo> entradas = new Lista<EntradaConsumo>();
            foreach (ZonaVerde zona in _zonas.GetAll().Where(z => z.Activo))
            {
                Lista<Lectura[]> medidores = new Lista<Lectura[]>();
                foreach (Sensor s in _sensores.GetByZona(zona.Id).Where(s => s.Tipo == TipoSensor.Caudal))
                {
                    Lectura[] lecturas = _lecturas.GetBySensor(s.Id)
                        .Where(l => l.FechaHora >= desde && l.FechaHora <= hasta)
                        .ToArray();
                    if (!Ordenamiento.EstaOrdenado(lecturas, ConsultaLecturas.PorFecha))
                    {
                        Ordenamiento.MergeSort(lecturas, ConsultaLecturas.PorFecha);
                    }
                    // Aunque no tenga lecturas en el periodo, la zona SÍ tiene medidor (consumo 0).
                    medidores.Add(lecturas);
                }
                entradas.Add(new EntradaConsumo { Zona = zona, Medidores = medidores.ToArray() });
            }
            return entradas;
        }

        private DateTime? PrimeraFecha()
        {
            DateTime? primera = null;
            foreach (Lectura l in _lecturas.GetAll())
            {
                if (primera == null || l.FechaHora < primera)
                {
                    primera = l.FechaHora;
                }
            }
            return primera;
        }
    }
}
