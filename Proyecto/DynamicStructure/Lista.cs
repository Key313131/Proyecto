using Proyecto.Contracts;
using System.Collections;

namespace Proyecto.DynamicStructure
{
    // Un eslabón de la lista: guarda un dato y conoce a su vecino de cada lado.
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Back { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }

    // Lista doblemente enlazada, basada en la del repositorio de Estructura de Datos.
    // Reemplaza a la List<T> de .NET dentro de los repositorios.
    public class Lista<T> : IEnumerable<T>, IListOperation<T> where T : class
    {
        private Node<T>? Head { get; set; }
        private Node<T>? Last { get; set; }

        public int Count { get; private set; }

        public bool IsEmpty
        {
            get
            {
                return Head == null;
            }
        }

        // O(1): se engancha al final gracias a Last; no hace falta recorrer.
        public void Add(T data)
        {
            Node<T> n = new Node<T>(data);
            if (IsEmpty)
            {
                Head = n;
                Last = n;
            }
            else
            {
                n.Back = Last;
                Last!.Next = n;
                Last = n;
            }
            Count++;
        }

        // O(n): hay que caminar hasta la posición.
        public void Delete(int position)
        {
            Node<T>? n = NodoEn(position);
            if (n != null)
            {
                Desenlazar(n);
            }
        }

        // O(n): busca el MISMO objeto (misma referencia) y lo quita.
        public void Delete(T data)
        {
            Node<T>? temp = Head;
            while (temp != null)
            {
                if (ReferenceEquals(temp.Data, data))
                {
                    Desenlazar(temp);
                    return;
                }
                temp = temp.Next;
            }
        }

        // O(n)
        public T? ElementAt(int position)
        {
            return NodoEn(position)?.Data;
        }

        // O(n)
        public void Set(int index, T data)
        {
            Node<T>? n = NodoEn(index);
            if (n != null)
            {
                n.Data = data;
            }
        }

        // O(n): devuelve el primero que cumpla la condición, o null.
        public T? FirstOrDefault(Predicate<T> check)
        {
            Node<T>? temp = Head;
            while (temp != null)
            {
                if (check(temp.Data))
                {
                    return temp.Data;
                }
                temp = temp.Next;
            }
            return null;
        }

        // O(n): devuelve la posición del primero que cumpla la condición, o -1.
        public int FindIndex(Predicate<T> check)
        {
            Node<T>? temp = Head;
            int index = 0;
            while (temp != null)
            {
                if (check(temp.Data))
                {
                    return index;
                }
                temp = temp.Next;
                index++;
            }
            return -1;
        }

        // O(n)
        public bool Exists(Predicate<T> check)
        {
            return FindIndex(check) != -1;
        }

        // Permite usar foreach (y LINQ) sobre la lista: recorre de Head a Last.
        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? temp = Head;
            while (temp != null)
            {
                yield return temp.Data;
                temp = temp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Camina desde Head hasta la posición pedida. Fuera de rango devuelve null.
        private Node<T>? NodoEn(int position)
        {
            if (position < 0 || position >= Count)
            {
                return null;
            }

            Node<T>? temp = Head;
            for (int i = 0; i < position; i++)
            {
                temp = temp!.Next;
            }
            return temp;
        }

        // O(1): saca un nodo uniendo a sus dos vecinos entre sí.
        // Cubre los cuatro casos: único, primero, último y del medio.
        private void Desenlazar(Node<T> n)
        {
            if (n.Back == null)
            {
                Head = n.Next;       // era el primero
            }
            else
            {
                n.Back.Next = n.Next;
            }

            if (n.Next == null)
            {
                Last = n.Back;       // era el último
            }
            else
            {
                n.Next.Back = n.Back;
            }

            n.Next = null;
            n.Back = null;
            Count--;
        }
    }
}
