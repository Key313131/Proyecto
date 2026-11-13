using Proyecto.Algorithms;

namespace Proyecto.Pruebas
{
    public enum Algoritmo
    {
        MergeSort,
        QuickSort,
        Insercion,
        Burbuja
    }

    // Algoritmos de ordenamiento que se implementaron para COMPARARLOS con Merge Sort
    // (Tabla 2 del documento). El programa solo usa Merge Sort (Algorithms/Ordenamiento.cs),
    // por eso estos viven aquí, en las pruebas, y no en el programa.
    //
    // Igual que en el programa: trabajan sobre arreglos y reciben un Comparison<T>,
    // una función (a, b) que devuelve negativo si a va ANTES que b, 0 si son iguales
    // y positivo si a va DESPUÉS.
    internal static class OrdenamientosComparados
    {
        public static void Ordenar<T>(T[] datos, Comparison<T> comparar, Algoritmo algoritmo)
        {
            switch (algoritmo)
            {
                case Algoritmo.Burbuja:
                    Burbuja(datos, comparar);
                    break;
                case Algoritmo.Insercion:
                    Insercion(datos, comparar);
                    break;
                case Algoritmo.QuickSort:
                    QuickSort(datos, comparar);
                    break;
                default:
                    Ordenamiento.MergeSort(datos, comparar);     // el mismo que usa el programa
                    break;
            }
        }

        public static string BigO(Algoritmo algoritmo)
        {
            return algoritmo switch
            {
                Algoritmo.Burbuja => "O(n²), mejor caso O(n)",
                Algoritmo.Insercion => "O(n²), mejor caso O(n)",
                Algoritmo.QuickSort => "O(n log n) promedio, O(n²) peor caso",
                _ => "O(n log n) siempre"
            };
        }

        // ---------------- Burbuja ----------------
        // Compara cada par de vecinos y los intercambia si están al revés. En cada pasada
        // el mayor "sube" hasta el final, como una burbuja.
        // O(n²). Si en una pasada no hubo cambios ya está ordenado y se sale: mejor caso O(n).
        public static void Burbuja<T>(T[] datos, Comparison<T> comparar)
        {
            for (int fin = datos.Length - 1; fin > 0; fin--)
            {
                bool huboCambio = false;
                for (int i = 0; i < fin; i++)
                {
                    if (comparar(datos[i], datos[i + 1]) > 0)
                    {
                        Intercambiar(datos, i, i + 1);
                        huboCambio = true;
                    }
                }

                if (!huboCambio)
                {
                    return;
                }
            }
        }

        // ---------------- Inserción ----------------
        // Como ordenar cartas en la mano: se toma la siguiente y se corre hacia la izquierda
        // hasta encontrar su lugar entre las que ya están ordenadas.
        // O(n²). Si ya está casi ordenado, casi no corre nada: mejor caso O(n).
        public static void Insercion<T>(T[] datos, Comparison<T> comparar)
        {
            for (int i = 1; i < datos.Length; i++)
            {
                T actual = datos[i];
                int j = i - 1;

                // Corre una posición a la derecha a los que son mayores que "actual".
                while (j >= 0 && comparar(datos[j], actual) > 0)
                {
                    datos[j + 1] = datos[j];
                    j--;
                }
                datos[j + 1] = actual;
            }
        }

        // ---------------- QuickSort (rápido) ----------------
        // Elige un pivote y reparte: los menores a la izquierda y los mayores a la derecha.
        // Luego hace lo mismo con cada lado.
        // Promedio O(n log n). Peor caso O(n²), si el pivote cae siempre en un extremo.
        // No usa arreglo auxiliar, pero NO es estable.
        public static void QuickSort<T>(T[] datos, Comparison<T> comparar)
        {
            QuickSort(datos, 0, datos.Length - 1, comparar);
        }

        private static void QuickSort<T>(T[] datos, int inicio, int fin, Comparison<T> comparar)
        {
            while (inicio < fin)
            {
                int corte = Particionar(datos, inicio, fin, comparar);

                // La recursión va a la parte MÁS PEQUEÑA y la grande se sigue en este while.
                // Así nunca hay más de log n llamadas anidadas y no se desborda la pila.
                if (corte - inicio < fin - corte)
                {
                    QuickSort(datos, inicio, corte, comparar);
                    inicio = corte + 1;
                }
                else
                {
                    QuickSort(datos, corte + 1, fin, comparar);
                    fin = corte;
                }
            }
        }

        // Partición de Hoare con el pivote en el MEDIO.
        //  - Si el pivote fuera el primero o el último, un arreglo que ya viene ordenado
        //    caería en el peor caso O(n²).
        //  - Los iguales al pivote se reparten a ambos lados, así que muchos valores
        //    repetidos no lo vuelven lento.
        // Al terminar: datos[inicio..j] <= pivote <= datos[j+1..fin].
        private static int Particionar<T>(T[] datos, int inicio, int fin, Comparison<T> comparar)
        {
            T pivote = datos[inicio + (fin - inicio) / 2];
            int i = inicio - 1;
            int j = fin + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (comparar(datos[i], pivote) < 0);

                do
                {
                    j--;
                } while (comparar(datos[j], pivote) > 0);

                if (i >= j)
                {
                    return j;
                }
                Intercambiar(datos, i, j);
            }
        }

        private static void Intercambiar<T>(T[] datos, int a, int b)
        {
            T temporal = datos[a];
            datos[a] = datos[b];
            datos[b] = temporal;
        }

        // ---------------- Búsqueda lineal ----------------
        // Revisa uno por uno desde el principio. O(n). Se usa solo para comparar con la
        // búsqueda binaria del programa (Algorithms/Busqueda.Binaria).
        public static int BusquedaLineal<T>(T[] datos, Predicate<T> esElBuscado, out int comparaciones)
        {
            comparaciones = 0;
            for (int i = 0; i < datos.Length; i++)
            {
                comparaciones++;
                if (esElBuscado(datos[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
