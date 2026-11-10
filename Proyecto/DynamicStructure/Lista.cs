using Proyecto.Contracts;
using System.Collections;

namespace Proyecto.DynamicStructure
{
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

        public void Delete(int position)
        {
            Node<T>? n = NodoEn(position);
            if (n != null)
            {
                Desenlazar(n);
            }
        }

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

        public T? ElementAt(int position)
        {
            return NodoEn(position)?.Data;
        }

        public void Set(int index, T data)
        {
            Node<T>? n = NodoEn(index);
            if (n != null)
            {
                n.Data = data;
            }
        }


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


        public bool Exists(Predicate<T> check)
        {
            return FindIndex(check) != -1;
        }

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

        private void Desenlazar(Node<T> n)
        {
            if (n.Back == null)
            {
                Head = n.Next;       
            }
            else
            {
                n.Back.Next = n.Next;
            }

            if (n.Next == null)
            {
                Last = n.Back;       
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
