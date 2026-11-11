using Proyecto.Contracts;
using System.Collections;

namespace Proyecto.DynamicStructure
{

    public class NodeCola<T>
    {
        public T Data { get; set; }
        public NodeCola<T>? Next { get; set; }

        public NodeCola(T data)
        {
            Data = data;
        }
    }

    public class Cola<T> : IEnumerable<T>, ICola<T> where T : class
    {
        private NodeCola<T>? Front { get; set; }
        private NodeCola<T>? Back { get; set; }

        public int Count { get; private set; }

        public bool IsEmpty
        {
            get
            {
                return Front == null;
            }
        }

        public void Enqueue(T data)
        {
            NodeCola<T> n = new NodeCola<T>(data);
            if (IsEmpty)
            {
                Front = n;
            }
            else
            {
                Back!.Next = n;
            }
            Back = n;
            Count++;
        }

        public T? Dequeue()
        {
            if (IsEmpty)
            {
                return null;
            }

            NodeCola<T> salida = Front!;
            Front = salida.Next;
            if (Front == null)
            {
                Back = null;     
            }
            Count--;
            return salida.Data;
        }

        public T? Peek()
        {
            return Front?.Data;
        }

        public void Clear()
        {
            Front = null;
            Back = null;
            Count = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            NodeCola<T>? temp = Front;
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
    }
}
