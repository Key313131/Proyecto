namespace Proyecto.Algorithms
{
    // Ordenamiento hecho por nosotros (sin Array.Sort ni OrderBy). El programa usa Merge Sort
    // porque es O(n log n) siempre y es estable. Burbuja, Inserción y QuickSort también se
    // implementaron, pero solo para compararlos (Tabla 2 del documento): están en el
    // proyecto Pruebas, en OrdenamientosComparados.cs.
    //
    // Trabaja sobre arreglos y no sobre la Lista enlazada porque necesita saltar a
    // cualquier posición (datos[i]) en O(1). En la Lista, ElementAt(i) recorre nodo por
    // nodo, O(n), y el ordenamiento se volvería n veces más lento.
    //
    // Comparison<T> es un delegado: una función (a, b) que devuelve un número
    //   negativo si a va ANTES que b, 0 si son iguales y positivo si a va DESPUÉS.
    // Así el mismo algoritmo ordena por fecha, por valor o por lo que se quiera.
    internal static class Ordenamiento
    {
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

        private static void Intercambiar<T>(T[] datos, int a, int b)
        {
            T temporal = datos[a];
            datos[a] = datos[b];
            datos[b] = temporal;
        }
    }
}
