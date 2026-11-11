namespace Proyecto.Algorithms
{
    public enum Algoritmo
    {
        MergeSort,
        QuickSort,
        Insercion,
        Burbuja
    }

    // Algoritmos de ordenamiento hechos por nosotros (sin Array.Sort ni OrderBy).
    //
    // Trabajan sobre arreglos y no sobre la Lista enlazada porque necesitan saltar a
    // cualquier posición (datos[i]) en O(1). En la Lista, ElementAt(i) recorre nodo por
    // nodo, O(n), y cada algoritmo se volvería n veces más lento.
    //
    // Comparison<T> es un delegado: una función (a, b) que devuelve un número
    //   negativo si a va ANTES que b, 0 si son iguales y positivo si a va DESPUÉS.
    // Así el mismo algoritmo ordena por fecha, por valor o por lo que se quiera.
    internal static class Ordenamiento
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
                    MergeSort(datos, comparar);
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

        // Estable = si dos elementos son iguales, quedan en el mismo orden en que estaban.
        public static bool EsEstable(Algoritmo algoritmo)
        {
            return algoritmo != Algoritmo.QuickSort;
        }

        public static bool EsCuadratico(Algoritmo algoritmo)
        {
            return algoritmo == Algoritmo.Burbuja || algoritmo == Algoritmo.Insercion;
        }

        // Revisa si ya está en orden. O(n).
        public static bool EstaOrdenado<T>(T[] datos, Comparison<T> comparar)
        {
            for (int i = 1; i < datos.Length; i++)
            {
                if (comparar(datos[i - 1], datos[i]) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Le da la vuelta al arreglo: el primero queda último. O(n), intercambiando las puntas.
        public static void Invertir<T>(T[] datos)
        {
            int i = 0;
            int j = datos.Length - 1;
            while (i < j)
            {
                Intercambiar(datos, i, j);
                i++;
                j--;
            }
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

        // ---------------- MergeSort (mezcla) ----------------
        // Divide y vencerás: parte el arreglo en dos mitades, ordena cada una (recursión)
        // y luego las mezcla en una sola.
        // Hay log n niveles de división y en cada nivel se mezclan n elementos: O(n log n)
        // en todos los casos. Necesita un arreglo auxiliar del mismo tamaño: O(n) de memoria.
        public static void MergeSort<T>(T[] datos, Comparison<T> comparar)
        {
            if (datos.Length < 2)
            {
                return;
            }

            T[] auxiliar = new T[datos.Length];
            MergeSort(datos, auxiliar, 0, datos.Length - 1, comparar);
        }

        private static void MergeSort<T>(T[] datos, T[] auxiliar, int inicio, int fin, Comparison<T> comparar)
        {
            if (inicio >= fin)
            {
                return;     // un solo elemento ya está ordenado (caso base)
            }

            int medio = inicio + (fin - inicio) / 2;
            MergeSort(datos, auxiliar, inicio, medio, comparar);
            MergeSort(datos, auxiliar, medio + 1, fin, comparar);
            Mezclar(datos, auxiliar, inicio, medio, fin, comparar);
        }

        private static void Mezclar<T>(T[] datos, T[] auxiliar, int inicio, int medio, int fin, Comparison<T> comparar)
        {
            // Si el último de la izquierda no es mayor que el primero de la derecha,
            // las dos mitades ya están en orden y no hay nada que mezclar.
            if (comparar(datos[medio], datos[medio + 1]) <= 0)
            {
                return;
            }

            Array.Copy(datos, inicio, auxiliar, inicio, fin - inicio + 1);

            int izq = inicio;        // recorre la mitad izquierda
            int der = medio + 1;     // recorre la mitad derecha
            int pos = inicio;        // dónde se escribe en "datos"

            while (izq <= medio && der <= fin)
            {
                // Con "<=" gana el de la izquierda cuando son iguales: eso lo hace ESTABLE.
                if (comparar(auxiliar[izq], auxiliar[der]) <= 0)
                {
                    datos[pos] = auxiliar[izq];
                    izq++;
                }
                else
                {
                    datos[pos] = auxiliar[der];
                    der++;
                }
                pos++;
            }

            // Lo que sobró de la izquierda se copia al final.
            // Lo que sobra de la derecha ya está en su lugar dentro de "datos".
            while (izq <= medio)
            {
                datos[pos] = auxiliar[izq];
                izq++;
                pos++;
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
        //    (como las lecturas, que llegan por fecha) caería en el peor caso O(n²).
        //  - Los iguales al pivote se reparten a ambos lados, así que muchos valores repetidos
        //    (caudal 0 cuando no se riega) no lo vuelven lento.
        // Al terminar: datos[inicio..j] <= pivote <= datos[j+1..fin].
        private static int Particionar<T>(T[] datos, int inicio, int fin, Comparison<T> comparar)
        {
            T pivote = datos[inicio + (fin - inicio) / 2];
            int i = inicio - 1;
            int j = fin + 1;

            while (true)
            {
                // Avanza i hasta uno que NO sea menor que el pivote.
                do
                {
                    i++;
                } while (comparar(datos[i], pivote) < 0);

                // Retrocede j hasta uno que NO sea mayor que el pivote.
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
    }
}
