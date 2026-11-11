using Proyecto.DynamicStructure;

namespace Proyecto.Algorithms
{
    // Algoritmos de búsqueda propios. Cada uno cuenta cuántas comparaciones hizo
    // (parámetro out) para poder mostrar la diferencia entre O(n) y O(log n).
    internal static class Busqueda
    {
        // Búsqueda lineal: revisa uno por uno desde el principio.
        // O(n). Funciona aunque los datos estén desordenados.
        // Devuelve la posición del primero que cumple, o -1 si ninguno.
        public static int Lineal<T>(T[] datos, Predicate<T> esElBuscado, out int comparaciones)
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

        // Búsqueda binaria: SOLO sirve si los datos están ordenados.
        // Mira el del medio: si es el buscado, termina; si es menor, el buscado está a la
        // derecha; si es mayor, está a la izquierda. Cada vez descarta la mitad.
        // O(log n): con 1 000 000 de datos bastan unas 20 comparaciones.
        //
        // "compararConBuscado" recibe un elemento y dice si es menor (negativo), igual (0)
        // o mayor (positivo) que lo que se busca.
        public static int Binaria<T>(T[] ordenado, Func<T, int> compararConBuscado, out int comparaciones)
        {
            comparaciones = 0;
            int inicio = 0;
            int fin = ordenado.Length - 1;

            while (inicio <= fin)
            {
                int medio = inicio + (fin - inicio) / 2;
                int resultado = compararConBuscado(ordenado[medio]);
                comparaciones++;

                if (resultado == 0)
                {
                    return medio;
                }
                if (resultado < 0)
                {
                    inicio = medio + 1;     // el del medio es menor: buscar a la derecha
                }
                else
                {
                    fin = medio - 1;        // el del medio es mayor: buscar a la izquierda
                }
            }
            return -1;
        }

        // Variante de la binaria: primera posición donde la condición se vuelve verdadera.
        // La condición debe ser "monótona" sobre los datos ordenados, es decir:
        //   falso, falso, ..., falso, verdadero, verdadero, ..., verdadero
        // Ejemplo: "FechaHora >= desde" sobre lecturas ordenadas por fecha.
        // Si ninguno cumple, devuelve ordenado.Length. O(log n).
        public static int PrimeroQueCumple<T>(T[] ordenado, Predicate<T> condicion)
        {
            int inicio = 0;
            int fin = ordenado.Length;      // "fin" es una posición que sí cumple (o el final)

            while (inicio < fin)
            {
                int medio = inicio + (fin - inicio) / 2;
                if (condicion(ordenado[medio]))
                {
                    fin = medio;            // cumple: la respuesta es medio o algo a su izquierda
                }
                else
                {
                    inicio = medio + 1;     // no cumple: la respuesta está a la derecha
                }
            }
            return inicio;
        }

        // Filtro lineal entre las posiciones [inicio, fin): guarda los que cumplen.
        // O(k), con k = fin - inicio. Se usa nuestra Lista porque no se sabe cuántos
        // van a cumplir; al final se pasa a un arreglo para poder ordenarlo.
        public static T[] Filtrar<T>(T[] datos, int inicio, int fin, Predicate<T> condicion) where T : class
        {
            Lista<T> encontrados = new Lista<T>();
            for (int i = inicio; i < fin; i++)
            {
                if (condicion(datos[i]))
                {
                    encontrados.Add(datos[i]);
                }
            }
            return AArreglo(encontrados);
        }

        // Copia la Lista a un arreglo recorriéndola una sola vez. O(n).
        public static T[] AArreglo<T>(Lista<T> lista) where T : class
        {
            T[] arreglo = new T[lista.Count];
            int i = 0;
            foreach (T elemento in lista)
            {
                arreglo[i] = elemento;
                i++;
            }
            return arreglo;
        }
    }
}
