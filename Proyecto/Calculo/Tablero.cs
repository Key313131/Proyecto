using Proyecto.Algorithms;
using Proyecto.Domain;
using Proyecto.DynamicStructure;

namespace Proyecto.Calculo
{
    public enum CriterioTablero
    {
        Zona,
        Consumo,
        Eficiencia,
        Ahorro
    }

    internal class ParametrosTablero
    {
        public DateTime Desde { get; init; }
        public DateTime Hasta { get; init; }
        public double Tarifa { get; init; } = 5000;             // $ por m³
        public double LaminaTradicional { get; init; } = 5;     // mm por día (= L/m² por día)
        public double MetaAhorro { get; init; } = 20;           // %
        public CriterioTablero Orden { get; init; } = CriterioTablero.Zona;
        public bool Descendente { get; init; }
    }

    // Lo que se midió en una zona en el periodo: las lecturas de cada medidor de caudal,
    // ya ordenadas por fecha.
    internal class EntradaConsumo
    {
        public ZonaVerde Zona { get; init; } = null!;
        public Lectura[][] Medidores { get; init; } = Array.Empty<Lectura[]>();
    }

    internal class FilaTablero
    {
        public ZonaVerde Zona { get; init; } = null!;
        public bool TieneMedidor { get; init; }
        public int Lecturas { get; init; }
        public double Litros { get; init; }             // consumo real
        public double LitrosPorM2 { get; init; }        // eficiencia
        public double Costo { get; init; }
        public double Tradicional { get; init; }        // lo que se habría gastado con lámina fija
        public double Ahorro { get; init; }             // %
        public bool CumpleMeta { get; init; }
    }

    internal class Tablero
    {
        public Lista<FilaTablero> Zonas { get; } = new Lista<FilaTablero>();
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public double Dias { get; set; }
        public double Litros { get; set; }
        public double Costo { get; set; }
        public double Area { get; set; }                // de las zonas con medidor
        public double LitrosPorM2 { get; set; }
        public double Tradicional { get; set; }
        public double Ahorro { get; set; }              // %
        public bool CumpleMeta { get; set; }
        public int ZonasSinMedidor { get; set; }
    }

    // Indicadores del dashboard: consumo, costo, eficiencia (L/m²) y ahorro frente a
    // un riego tradicional de lámina fija (regar todos los días lo mismo, sin mirar sensores).
    internal static class CalculoTablero
    {
        // Un tramo de más de 2 pasos del simulador (20 min) sin lecturas se toma como hueco.
        public static readonly TimeSpan HuecoMaximo = TimeSpan.FromMinutes(20);

        public static Tablero Calcular(IEnumerable<EntradaConsumo> entradas, ParametrosTablero p)
        {
            Tablero t = new Tablero { Desde = p.Desde, Hasta = p.Hasta };
            t.Dias = Math.Max(0, (p.Hasta - p.Desde).TotalDays);

            Lista<FilaTablero> filas = new Lista<FilaTablero>();

            foreach (EntradaConsumo e in entradas)
            {
                double area = e.Zona.AreaM2;
                bool tieneMedidor = e.Medidores.Length > 0;
                double litros = 0;
                int lecturas = 0;

                // Si la zona tiene varios medidores, todos miden el mismo tubo: se promedian.
                if (tieneMedidor)
                {
                    double suma = 0;
                    foreach (Lectura[] medidor in e.Medidores)
                    {
                        ResultadoConsumo c = Consumo.Litros(medidor, HuecoMaximo);
                        suma += c.Litros;
                        lecturas += medidor.Length;
                    }
                    litros = suma / e.Medidores.Length;
                }
                else
                {
                    t.ZonasSinMedidor++;
                }

                double tradicional = tieneMedidor ? p.LaminaTradicional * area * t.Dias : 0;
                double ahorro = tradicional > 0 ? (tradicional - litros) / tradicional * 100 : 0;
                filas.Add(new FilaTablero
                {
                    Zona = e.Zona,
                    TieneMedidor = tieneMedidor,
                    Lecturas = lecturas,
                    Litros = litros,
                    LitrosPorM2 = litros / area,
                    Costo = litros / 1000 * p.Tarifa,
                    Tradicional = tradicional,
                    Ahorro = ahorro,
                    CumpleMeta = tieneMedidor && tradicional > 0 && ahorro >= p.MetaAhorro
                });

                if (tieneMedidor)
                {
                    t.Litros += litros;
                    t.Area += area;
                    t.Tradicional += tradicional;
                }
            }

            t.Costo = t.Litros / 1000 * p.Tarifa;
            t.LitrosPorM2 = t.Area > 0 ? t.Litros / t.Area : 0;
            t.Ahorro = t.Tradicional > 0 ? (t.Tradicional - t.Litros) / t.Tradicional * 100 : 0;
            t.CumpleMeta = t.Tradicional > 0 && t.Ahorro >= p.MetaAhorro;

            // Orden pedido por el usuario, con nuestro MergeSort (estable).
            FilaTablero[] orden = filas.ToArray();
            Comparison<FilaTablero> criterio = Comparador(p.Orden);
            Comparison<FilaTablero> comparar = p.Descendente ? (a, b) => criterio(b, a) : criterio;
            if (!Ordenamiento.EstaOrdenado(orden, comparar))
            {
                Ordenamiento.MergeSort(orden, comparar);
            }
            foreach (FilaTablero f in orden)
            {
                t.Zonas.Add(f);
            }

            return t;
        }

        public static Comparison<FilaTablero> Comparador(CriterioTablero criterio)
        {
            return criterio switch
            {
                CriterioTablero.Consumo => (a, b) => a.Litros.CompareTo(b.Litros),
                CriterioTablero.Eficiencia => (a, b) => a.LitrosPorM2.CompareTo(b.LitrosPorM2),
                CriterioTablero.Ahorro => (a, b) => a.Ahorro.CompareTo(b.Ahorro),
                _ => (a, b) => a.Zona.Id.CompareTo(b.Zona.Id)
            };
        }
    }
}
