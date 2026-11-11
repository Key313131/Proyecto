using Proyecto.Domain;
using Proyecto.DynamicStructure;
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
        public Algoritmo Algoritmo { get; init; } = Algoritmo.MergeSort;
        public bool Descendente { get; init; }
    }

    internal class ResultadoConsulta
    {
        public Lectura[] Lecturas { get; set; } = Array.Empty<Lectura>();
        public Lista<string> Detalle { get; } = new Lista<string>();
        public int Hilo { get; set; }               // en qué hilo se calculó
        public double Milisegundos { get; set; }
    }

    internal class FilaComparacion
    {
        public Algoritmo Algoritmo { get; init; }
        public bool Omitido { get; init; }
        public long ComparacionesAzar { get; init; }
        public double MsAzar { get; init; }
        public long ComparacionesOrdenado { get; init; }
        public double MsOrdenado { get; init; }
        public bool Correcto { get; init; }
    }

    // Consultas sobre las lecturas: rango de fechas, filtro por sensor y ordenamiento.
    //
    // IMPORTANTE: estos métodos se ejecutan con Task.Run, en un hilo distinto al de la
    // interfaz. Por eso NO tocan el repositorio ni ningún control: reciben una COPIA de
    // las lecturas (un arreglo hecho en el hilo de la interfaz) y solo trabajan con ella.
    // Así el simulador puede seguir agregando lecturas al repositorio al mismo tiempo
    // sin que dos hilos toquen la misma Lista.
    internal static class ConsultaLecturas
    {
        // Burbuja e Inserción son O(n²): con más elementos tardarían demasiado.
        public const int LimiteCuadratico = 30000;

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

        // Pasos que da la búsqueda binaria en el peor caso: log₂(n), redondeado hacia arriba.
        public static int PasosBinaria(int n)
        {
            return n <= 0 ? 0 : (int)Math.Ceiling(Math.Log2(n + 1));
        }

        // "copia" debe ser un arreglo propio: este método puede reordenarlo.
        public static ResultadoConsulta Ejecutar(Lectura[] copia, FiltroConsulta filtro)
        {
            ResultadoConsulta r = new ResultadoConsulta();
            r.Hilo = Environment.CurrentManagedThreadId;
            Stopwatch total = Stopwatch.StartNew();
            int n = copia.Length;

            // 1. La búsqueda binaria necesita las lecturas ordenadas por fecha. Normalmente ya
            //    lo están (el simulador las genera en orden), así que basta con revisarlo.
            if (Ordenamiento.EstaOrdenado(copia, PorFecha))
            {
                r.Detalle.Add($"1. Orden por fecha verificado en {n:N0} lecturas: O(n)");
            }
            else
            {
                Ordenamiento.MergeSort(copia, PorFecha);
                r.Detalle.Add($"1. Las lecturas no estaban por fecha: se ordenaron con MergeSort, O(n log n)");
            }

            // 2. Rango de fechas con búsqueda binaria: dónde empieza y dónde termina.
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

            if (filtro.Desde != null || filtro.Hasta != null)
            {
                r.Detalle.Add($"2. Rango de fechas con búsqueda binaria: {fin - inicio:N0} de {n:N0} lecturas, "
                    + $"unos {PasosBinaria(n)} pasos por extremo: O(log n)");
            }
            else
            {
                r.Detalle.Add("2. Sin filtro de fechas: se toman todas");
            }

            // 3. Filtro por sensor, recorriendo solo el rango: lineal.
            long? sensor = filtro.SensorId;
            Lectura[] resultado = Busqueda.Filtrar(copia, inicio, fin, l => sensor == null || l.SensorId == sensor);
            r.Detalle.Add(sensor == null
                ? $"3. Todos los sensores: {resultado.Length:N0} lecturas"
                : $"3. Filtro por sensor {sensor} (búsqueda lineal en el rango): {resultado.Length:N0} lecturas, O(k)");

            // 4. Ordenar con el algoritmo elegido, contando las comparaciones.
            Algoritmo algoritmo = filtro.Algoritmo;
            if (Ordenamiento.EsCuadratico(algoritmo) && resultado.Length > LimiteCuadratico)
            {
                r.Detalle.Add($"   {algoritmo} con {resultado.Length:N0} lecturas tardaría demasiado (O(n²)): se usa MergeSort");
                algoritmo = Algoritmo.MergeSort;
            }

            Comparison<Lectura> criterio = Comparador(filtro.Criterio);
            Comparison<Lectura> ordenar = criterio;
            if (filtro.Descendente)
            {
                ordenar = (a, b) => criterio(b, a);     // invertir los argumentos invierte el orden
            }

            long comparaciones = 0;
            Comparison<Lectura> contada = (a, b) =>
            {
                comparaciones++;
                return ordenar(a, b);
            };

            Stopwatch tiempo = Stopwatch.StartNew();
            Ordenamiento.Ordenar(resultado, contada, algoritmo);
            tiempo.Stop();

            string sentido = filtro.Descendente ? " (descendente)" : "";
            r.Detalle.Add($"4. Ordenado por {filtro.Criterio}{sentido} con {algoritmo}: {comparaciones:N0} comparaciones "
                + $"en {tiempo.Elapsed.TotalMilliseconds:0.##} ms. {Ordenamiento.BigO(algoritmo)}");

            if (!Ordenamiento.EsEstable(algoritmo) && filtro.Criterio != CriterioOrden.Fecha)
            {
                r.Detalle.Add("   QuickSort no es estable: entre lecturas iguales, las fechas pueden quedar mezcladas");
            }

            r.Lecturas = resultado;
            r.Milisegundos = total.Elapsed.TotalMilliseconds;
            return r;
        }

        // Busca una lectura por Id de las dos formas para comparar.
        public static ResultadoConsulta BuscarPorId(Lectura[] copia, long id)
        {
            ResultadoConsulta r = new ResultadoConsulta();
            r.Hilo = Environment.CurrentManagedThreadId;
            Stopwatch total = Stopwatch.StartNew();

            // La primera vez que se llama un método, .NET lo compila (JIT) y eso tarda más que
            // la búsqueda misma. Se llama una vez antes de medir para que el tiempo sea justo.
            Predicate<Lectura> esElBuscado = l => l.Id == id;
            Func<Lectura, int> compararConBuscado = l => l.Id.CompareTo(id);
            Busqueda.Lineal(copia, esElBuscado, out _);
            Busqueda.Binaria(copia, compararConBuscado, out _);

            Stopwatch tiempo = Stopwatch.StartNew();
            int lineal = Busqueda.Lineal(copia, esElBuscado, out int pasosLineal);
            double usLineal = tiempo.Elapsed.TotalMilliseconds * 1000;

            // La binaria necesita los datos ordenados por Id.
            Lectura[] porId = copia;
            if (!Ordenamiento.EstaOrdenado(copia, PorId))
            {
                porId = (Lectura[])copia.Clone();
                Ordenamiento.MergeSort(porId, PorId);
                r.Detalle.Add("   Las lecturas no estaban por Id: se ordenó una copia con MergeSort antes de la binaria");
            }

            tiempo.Restart();
            int binaria = Busqueda.Binaria(porId, compararConBuscado, out int pasosBinaria);
            double usBinaria = tiempo.Elapsed.TotalMilliseconds * 1000;

            r.Detalle.Add($"Búsqueda lineal: {pasosLineal:N0} comparaciones, {usLineal:0.#} µs. O(n)");
            r.Detalle.Add($"Búsqueda binaria: {pasosBinaria:N0} comparaciones, {usBinaria:0.#} µs. O(log n)");

            if (binaria >= 0)
            {
                r.Lecturas = new[] { porId[binaria] };
                r.Detalle.Add($"Encontrada la lectura {id} (posición {lineal + 1:N0} de {copia.Length:N0})");
            }
            else
            {
                r.Detalle.Add($"No existe la lectura {id}");
            }

            r.Milisegundos = total.Elapsed.TotalMilliseconds;
            return r;
        }

        // Ordena n lecturas inventadas (por valor) con cada algoritmo, primero al azar y luego
        // ya ordenadas, y mide comparaciones y tiempo. "progreso" va avisando a la interfaz.
        public static Lista<FilaComparacion> Comparar(int n, IProgress<string>? progreso = null)
        {
            Lista<FilaComparacion> filas = new Lista<FilaComparacion>();
            Random azar = new Random(n);    // misma semilla: mismos datos para todos los algoritmos
            DateTime inicio = new DateTime(2026, 1, 1);

            Lectura[] original = new Lectura[n];
            for (int i = 0; i < n; i++)
            {
                original[i] = Lectura.Create(i + 1, 1, inicio.AddMinutes(i), Math.Round(azar.NextDouble() * 100, 2));
            }

            Comparison<Lectura> porValor = Comparador(CriterioOrden.Valor);
            progreso?.Report($"Comparando con n = {n:N0} lecturas al azar, ordenando por valor (hilo #{Environment.CurrentManagedThreadId})");

            foreach (Algoritmo algoritmo in Enum.GetValues<Algoritmo>())
            {
                if (Ordenamiento.EsCuadratico(algoritmo) && n > LimiteCuadratico)
                {
                    filas.Add(new FilaComparacion { Algoritmo = algoritmo, Omitido = true });
                    progreso?.Report($"{algoritmo}: omitido, con n = {n:N0} haría unas {(long)n * n / 2:N0} comparaciones");
                    continue;
                }

                Lectura[] datos = (Lectura[])original.Clone();
                (long compAzar, double msAzar) = Medir(datos, porValor, algoritmo);
                bool correcto = Ordenamiento.EstaOrdenado(datos, porValor);

                // Otra vez sobre el arreglo que ya quedó ordenado: el "mejor caso".
                (long compOrdenado, double msOrdenado) = Medir(datos, porValor, algoritmo);

                filas.Add(new FilaComparacion
                {
                    Algoritmo = algoritmo,
                    ComparacionesAzar = compAzar,
                    MsAzar = msAzar,
                    ComparacionesOrdenado = compOrdenado,
                    MsOrdenado = msOrdenado,
                    Correcto = correcto
                });

                progreso?.Report($"{algoritmo}: al azar {msAzar:0.##} ms ({compAzar:N0} comp.) | ya ordenado {msOrdenado:0.##} ms "
                    + $"({compOrdenado:N0} comp.). {Ordenamiento.BigO(algoritmo)}{(correcto ? "" : "  ¡NO QUEDÓ ORDENADO!")}");
            }

            double nLogN = n * Math.Log2(Math.Max(n, 2));
            progreso?.Report($"Referencia: n² = {(long)n * n:N0}   n·log₂n = {nLogN:N0}");
            return filas;
        }

        private static (long comparaciones, double ms) Medir(Lectura[] datos, Comparison<Lectura> comparar, Algoritmo algoritmo)
        {
            long comparaciones = 0;
            Comparison<Lectura> contada = (a, b) =>
            {
                comparaciones++;
                return comparar(a, b);
            };

            Stopwatch tiempo = Stopwatch.StartNew();
            Ordenamiento.Ordenar(datos, contada, algoritmo);
            return (comparaciones, tiempo.Elapsed.TotalMilliseconds);
        }
    }
}
