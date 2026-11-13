using Proyecto.Domain;
using System.Diagnostics;

namespace Proyecto.Algorithms
{
    public enum CriterioOrden
    {
        Fecha,
        Valor,
        Sensor
    }

    // Lo que el usuario eligió en la pantalla de consultas.
    internal class FiltroConsulta
    {
        public long? SensorId { get; init; }        // null = todos los sensores
        public DateTime? Desde { get; init; }       // null = sin límite
        public DateTime? Hasta { get; init; }
        public CriterioOrden Criterio { get; init; } = CriterioOrden.Fecha;
        public bool Descendente { get; init; }
    }

    internal class ResultadoConsulta
    {
        public Lectura[] Lecturas { get; set; } = Array.Empty<Lectura>();
        public double Milisegundos { get; set; }
    }

    // Consultas sobre las lecturas: rango de fechas, filtro por sensor y ordenamiento.
    //
    // IMPORTANTE: estos métodos se ejecutan con Task.Run, en un hilo distinto al de la
    // interfaz. Por eso NO tocan el repositorio ni ningún control: reciben una COPIA de
    // las lecturas (un arreglo hecho en el hilo de la interfaz) y solo trabajan con ella.
    // Así el simulador puede seguir agregando lecturas al repositorio al mismo tiempo
    // sin que dos hilos toquen la misma Lista.
    //
    // Las pruebas de estos métodos y la comparación de algoritmos (Tabla 2) están en el
    // proyecto Pruebas.
    internal static class ConsultaLecturas
    {
        // Orden "natural" de las lecturas: por fecha y, si la fecha es igual, por Id.
        public static readonly Comparison<Lectura> PorFecha = (a, b) =>
        {
            int resultado = a.FechaHora.CompareTo(b.FechaHora);
            if (resultado != 0)
            {
                return resultado;
            }
            return a.Id.CompareTo(b.Id);
        };

        public static readonly Comparison<Lectura> PorId = (a, b) => a.Id.CompareTo(b.Id);

        public static Comparison<Lectura> Comparador(CriterioOrden criterio)
        {
            return criterio switch
            {
                CriterioOrden.Valor => (a, b) => a.Valor.CompareTo(b.Valor),
                CriterioOrden.Sensor => (a, b) => a.SensorId.CompareTo(b.SensorId),
                _ => PorFecha
            };
        }

        // "copia" debe ser un arreglo propio: este método puede reordenarlo.
        public static ResultadoConsulta Ejecutar(Lectura[] copia, FiltroConsulta filtro)
        {
            Stopwatch total = Stopwatch.StartNew();
            int n = copia.Length;

            // 1. La búsqueda binaria necesita las lecturas ordenadas por fecha. Normalmente ya
            //    lo están (el simulador las genera en orden), así que basta con revisarlo: O(n).
            if (!Ordenamiento.EstaOrdenado(copia, PorFecha))
            {
                Ordenamiento.MergeSort(copia, PorFecha);
            }

            // 2. Rango de fechas con búsqueda binaria: dónde empieza y dónde termina. O(log n).
            int inicio = 0;
            int fin = n;
            if (filtro.Desde != null)
            {
                DateTime desde = filtro.Desde.Value;
                inicio = Busqueda.PrimeroQueCumple(copia, l => l.FechaHora >= desde);
            }
            if (filtro.Hasta != null)
            {
                DateTime hasta = filtro.Hasta.Value;
                fin = Busqueda.PrimeroQueCumple(copia, l => l.FechaHora > hasta);
            }
            if (fin < inicio)
            {
                fin = inicio;
            }

            // 3. Filtro por sensor, recorriendo solo el rango: lineal, O(k).
            long? sensor = filtro.SensorId;
            Lectura[] resultado = Busqueda.Filtrar(copia, inicio, fin, l => sensor == null || l.SensorId == sensor);

            // 4. Ordenar, pero solo si hace falta.
            Comparison<Lectura> criterio = Comparador(filtro.Criterio);
            Comparison<Lectura> ordenar = criterio;
            if (filtro.Descendente)
            {
                ordenar = (a, b) => criterio(b, a);     // invertir los argumentos invierte el orden
            }

            // 4a. ¿Ya está en el orden pedido? Revisarlo cuesta O(n), ordenar O(n log n).
            //     Las lecturas llegan por fecha, así que "por fecha" casi nunca necesita ordenarse.
            if (Ordenamiento.EstaOrdenado(resultado, ordenar))
            {
                // nada que hacer
            }
            // 4b. Se pidió por fecha descendente y están por fecha ascendente: basta con darles
            //     la vuelta, O(n). (Sirve porque fecha + Id nunca se repiten: no hay empates.)
            else if (filtro.Criterio == CriterioOrden.Fecha && Ordenamiento.EstaOrdenado(resultado, criterio))
            {
                Ordenamiento.Invertir(resultado);
            }
            // 4c. Hay que ordenar de verdad: Merge Sort, O(n log n) y estable.
            else
            {
                Ordenamiento.MergeSort(resultado, ordenar);
            }

            return new ResultadoConsulta
            {
                Lecturas = resultado,
                Milisegundos = total.Elapsed.TotalMilliseconds
            };
        }

        // Busca una lectura por Id con búsqueda binaria, O(log n).
        public static ResultadoConsulta BuscarPorId(Lectura[] copia, long id)
        {
            Stopwatch total = Stopwatch.StartNew();

            // La binaria necesita los datos ordenados por Id. Las lecturas se crean con Id
            // creciente, así que casi siempre ya lo están.
            Lectura[] porId = copia;
            if (!Ordenamiento.EstaOrdenado(copia, PorId))
            {
                porId = (Lectura[])copia.Clone();
                Ordenamiento.MergeSort(porId, PorId);
            }

            int posicion = Busqueda.Binaria(porId, l => l.Id.CompareTo(id), out _);

            return new ResultadoConsulta
            {
                Lecturas = posicion >= 0 ? new[] { porId[posicion] } : Array.Empty<Lectura>(),
                Milisegundos = total.Elapsed.TotalMilliseconds
            };
        }
    }
}
