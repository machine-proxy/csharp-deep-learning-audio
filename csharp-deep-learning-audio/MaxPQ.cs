using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_deep_learning_audio
{
    public class MaxPQ<T> where T: IComparable<T>
    {
        private T[] s;
        private int N = 0;
        public MaxPQ(int capacity=10)
        {
            s = new T[capacity];
        }

        public void Add(T x)
        {
            if(s.Length == N+1)
            {
                Resize(s.Length * 2);
            }
            s[++N] = x;
            Swim(N);
        }

        public int Count
        {
            get { return N; }
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public T Peek()
        {
            if(Count==0)
            {
                return default(T);
            }
            return s[1];
        }

        public T DelMax()
        {
            if(Count == 0)
            {
                return default(T);
            }

            T val = s[1];
            Swap(s, N--, 1);
            Sink(1);
            s[N + 1] = default(T);
            if(N == s.Length / 4)
            {
                Resize(s.Length / 2);
            }
            return val;
        }

        private void Sink(int k)
        {
            while(k * 2 <= N)
            {
                int child = k * 2;
                if(child+1 <= N && GreaterThan(s[child+1], s[child])) {
                    child++;
                }
                if(GreaterThan(s[child], s[k]))
                {
                    Swap(s, child, k);
                    k = child;
                } else
                {
                    break;
                }
            }
        }

        private void Swim(int k)
        {
            while(k > 1)
            {
                int parent = k / 2;
                if(GreaterThan(s[k], s[parent]))
                {
                    Swap(s, k, parent);
                    k = parent;
                } else
                {
                    break;
                }
            }
        }

        

        private static void Swap(T[] a, int i, int j)
        {
            T temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        private static bool GreaterThan(T i, T j)
        {
            return i.CompareTo(j) > 0;
        }

        private void Resize(int newSize)
        {
            T[] temp = new T[newSize];
            int len = Math.Min(newSize, s.Length);
            for(int i=0; i < len; ++i)
            {
                temp[i] = s[i];
            }
            s = temp;
        }

        public List<T> ToList()
        {
            List<T> result = new List<T>();
            for(int i=0; i < N; ++i)
            {
                result.Add(s[i + 1]);
            }
            return result;
        }


    }
}
