using Proyecto.Algorithms;
using Proyecto.Domain;
using System.Diagnostics;

namespace Proyecto.Pruebas
{
    // Pruebas de EcoFacility. Se ejecutan con:
    //     dotnet run --project Pruebas
    //
    // 1. Revisa que los algoritmos ordenen bien.
    // 2. Revisa que las consultas del programa (filtro por sensor, rango de fechas y orden)
    //    den lo mismo que una solución "a la fuerza" recorriendo todo.
    // 3. Compara la búsqueda lineal con la binaria.
    // 4. Genera la comparación de algoritmos de la Tabla 2 del documento.
    // 5. Revisa la integral de flujo y el reparto de Lagrange (PruebasCalculo.cs).
    //
    // Al final dice cuántas pruebas fallaron (y termina con código 1 si alguna falló).
    internal static class Program
    {
        private static int _pasadas;
        private static int _fallidas;

        private static int Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Titulo("1. Los algoritmos ordenan bien");
            PruebaOrdenamientos();

            Titulo("2. Consultas de lecturas (las del programa)");
            PruebaConsultas();

            Titulo("3. Búsqueda lineal frente a binaria (n = 38.304, Id 20.000)");
            CompararBusquedas(38304, 20000);

            Titulo("4. Tabla 2: comparación de algoritmos con n = 25.000");
            CompararOrdenamientos(25000);

            Titulo("5. Cálculos: integral de flujo y reparto de Lagrange");
            PruebasCalculo.Ejecutar((correcto, prueba) => Revisar(correcto, prueba));

            Console.WriteLine();
            Console.WriteLine($"Resultado: {_pasadas} pruebas correctas, {_fallidas} fallidas.");
            return _fallidas == 0 ? 0 : 1;
        }

        // ------------------------------------------------------------------ 1
        private static void PruebaOrdenamientos()
        {
            Comparison<int> menorAMayor = (a, b) => a.CompareTo(b);
            Random azar = new Random(7);

            foreach (Algoritmo algoritmo in Enum.GetValues<Algoritmo>())
            {
                foreach (int n in new[] { 0, 1, 2, 10, 1000 })
                {
                    int[] datos = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        datos[i] = azar.Next(100);      // con repetidos a propósito
                    }

                    OrdenamientosComparados.Ordenar(datos, menorAMayor, algoritmo);
                    Revisar(Ordenamiento.EstaOrdenado(datos, menorAMayor), $"{algoritmo} ordena {n} números");
                }
            }

            // Merge Sort es estable: entre valores iguales se conserva el orden anterior (por Id).
            Lectura[] lecturas = Lecturas(3, 200);
            Ordenamiento.MergeSort(lecturas, (a, b) => a.Valor.CompareTo(b.Valor));
            bool estable = true;
            for (int i = 1; i < lecturas.Length; i++)
            {
                if (lecturas[i - 1].Valor == lecturas[i].Valor && lecturas[i - 1].Id > lecturas[i].Id)
                {
                    estable = false;
                }
            }
            Revisar(estable, "Merge Sort es estable (los valores iguales conservan su orden)");

            int[] alReves = { 5, 4, 3, 2, 1 };
            Ordenamiento.Invertir(alReves);
            Revisar(alReves.SequenceEqual(new[] { 1, 2, 3, 4, 5 }), "Invertir le da la vuelta al arreglo");
        }

        // ------------------------------------------------------------------ 2
        private static void PruebaConsultas()
        {
            Lectura[] todas = Lecturas(4, 500);     // 4 sensores, 500 lecturas cada uno
            DateTime desde = todas[300].FechaHora;
            DateTime hasta = todas[1500].FechaHora;

            // Sensor 2 entre dos fechas, por valor de mayor a menor.
            FiltroConsulta filtro = new FiltroConsulta
            {
                SensorId = 2,
                Desde = desde,
                Hasta = hasta,
                Criterio = CriterioOrden.Valor,
                Descendente = true
            };
            Lectura[] obtenidas = ConsultaLecturas.Ejecutar((Lectura[])todas.Clone(), filtro).Lecturas;

            // Lo mismo "a la fuerza": recorrer todo con LINQ.
            Lectura[] esperadas = todas
                .Where(l => l.SensorId == 2 && l.FechaHora >= desde && l.FechaHora <= hasta)
                .OrderByDescending(l => l.Valor)
                .ToArray();

            Revisar(obtenidas.Length == esperadas.Length,
                $"Filtro por sensor y fechas: {obtenidas.Length} lecturas (se esperaban {esperadas.Length})");
            Revisar(obtenidas.Select(l => l.Valor).SequenceEqual(esperadas.Select(l => l.Valor)),
                "Orden por valor descendente igual al esperado");

            // Todas, por fecha de la más nueva a la más vieja.
            FiltroConsulta porFecha = new FiltroConsulta { Criterio = CriterioOrden.Fecha, Descendente = true };
            Lectura[] recientes = ConsultaLecturas.Ejecutar((Lectura[])todas.Clone(), porFecha).Lecturas;
            Revisar(recientes.Length == todas.Length && recientes[0].Id == todas[^1].Id,
                "Orden por fecha descendente (la más reciente primero)");

            // Llegan desordenadas: el programa debe ordenarlas por fecha antes de la búsqueda binaria.
            Lectura[] revueltas = (Lectura[])todas.Clone();
            new Random(3).Shuffle(revueltas);
            Lectura[] desdeRevueltas = ConsultaLecturas.Ejecutar(revueltas, filtro).Lecturas;
            Revisar(desdeRevueltas.Length == esperadas.Length, "Con lecturas desordenadas el filtro de fechas sigue funcionando");

            // Buscar por Id.
            Lectura[] encontrada = ConsultaLecturas.BuscarPorId((Lectura[])todas.Clone(), 777).Lecturas;
            Revisar(encontrada.Length == 1 && encontrada[0].Id == 777, "Buscar por Id encuentra la lectura 777");
            Lectura[] noExiste = ConsultaLecturas.BuscarPorId((Lectura[])todas.Clone(), 999999).Lecturas;
            Revisar(noExiste.Length == 0, "Buscar por Id de una lectura que no existe no devuelve nada");
        }

        // ------------------------------------------------------------------ 3
        private static void CompararBusquedas(int n, long id)
        {
            Lectura[] lecturas = Lecturas(1, n);    // Ids 1..n, ordenadas como las del programa

            int lineal = OrdenamientosComparados.BusquedaLineal(lecturas, l => l.Id == id, out int pasosLineal);
            int binaria = Busqueda.Binaria(lecturas, l => l.Id.CompareTo(id), out int pasosBinaria);

            Console.WriteLine($"   Búsqueda lineal:  {pasosLineal,6:N0} comparaciones   O(n)");
            Console.WriteLine($"   Búsqueda binaria: {pasosBinaria,6:N0} comparaciones   O(log n)");
            Revisar(lineal == binaria && lineal >= 0, "Las dos búsquedas encuentran la misma lectura");
        }

        // ------------------------------------------------------------------ 4
        // Ordena n lecturas inventadas (por valor) con cada algoritmo, primero al azar y luego
        // ya ordenadas. Las comparaciones no dependen del equipo; los tiempos sí.
        private static void CompararOrdenamientos(int n)
        {
            Random azar = new Random(n);    // misma semilla: mismos datos para todos los algoritmos
            DateTime inicio = new DateTime(2026, 1, 1);
            Lectura[] original = new Lectura[n];
            for (int i = 0; i < n; i++)
            {
                original[i] = Lectura.Create(i + 1, 1, inicio.AddMinutes(i), Math.Round(azar.NextDouble() * 100, 2));
            }

            Comparison<Lectura> porValor = ConsultaLecturas.Comparador(CriterioOrden.Valor);
            Console.WriteLine($"   {"Algoritmo",-10} {"Complejidad",-38} {"Datos al azar",-32} {"Datos ya ordenados"}");

            foreach (Algoritmo algoritmo in Enum.GetValues<Algoritmo>())
            {
                Lectura[] datos = (Lectura[])original.Clone();
                (long compAzar, double msAzar) = Medir(datos, porValor, algoritmo);
                bool correcto = Ordenamiento.EstaOrdenado(datos, porValor);

                // Otra vez sobre el arreglo que ya quedó ordenado: el "mejor caso".
                (long compOrdenado, double msOrdenado) = Medir(datos, porValor, algoritmo);

                Console.WriteLine($"   {algoritmo,-10} {OrdenamientosComparados.BigO(algoritmo),-38} "
                    + $"{$"{compAzar:N0} comp. · {msAzar:N1} ms",-32} {compOrdenado:N0} comp. · {msOrdenado:N1} ms");
                Revisar(correcto, $"{algoritmo} dejó ordenadas las {n:N0} lecturas", silencioso: true);
            }

            Console.WriteLine($"   Referencia: n² = {(long)n * n:N0}   n·log₂n = {n * Math.Log2(n):N0}");
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
            OrdenamientosComparados.Ordenar(datos, contada, algoritmo);
            return (comparaciones, tiempo.Elapsed.TotalMilliseconds);
        }

        // ------------------------------------------------------------------ ayudas

        // Lecturas como las del simulador: cada 10 minutos, una por sensor, ordenadas por fecha.
        // Los valores son enteros de 0 a 49 para que haya repetidos.
        private static Lectura[] Lecturas(int sensores, int porSensor)
        {
            Random azar = new Random(11);
            DateTime inicio = new DateTime(2026, 10, 1);
            Lectura[] lecturas = new Lectura[sensores * porSensor];
            long id = 1;
            for (int paso = 0; paso < porSensor; paso++)
            {
                for (int s = 1; s <= sensores; s++)
                {
                    lecturas[id - 1] = Lectura.Create(id, s, inicio.AddMinutes(10 * paso), azar.Next(50));
                    id++;
                }
            }
            return lecturas;
        }

        private static void Revisar(bool correcto, string prueba, bool silencioso = false)
        {
            if (correcto)
            {
                _pasadas++;
                if (!silencioso)
                {
                    Console.WriteLine($"   [OK]    {prueba}");
                }
            }
            else
            {
                _fallidas++;
                Console.WriteLine($"   [FALLA] {prueba}");
            }
        }

        private static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine(texto);
        }
    }
}
