using System;
using System.Diagnostics;
using csharp_deep_learning_audio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace csharp_deep_learning_audio_unit
{
    [TestClass]
    public class MaxPQUnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            int[] a = Range(100);
            Shuffle(a);
            MaxPQ<int> pq = new MaxPQ<int>();
            pq.Add(10);
            Assert.AreEqual(10, pq.Peek());
            pq.Add(20);
            Assert.AreEqual(20, pq.Peek());
            for (int i = 0; i < 20; ++i)
            {
                pq.Add(i);
                Assert.AreEqual(i+3, pq.Count);
                Assert.AreEqual(20, pq.Peek());
            }
            Assert.AreEqual(20, pq.DelMax());
            Assert.AreEqual(19, pq.DelMax());
            foreach(int val in a)
            {
                pq.Add(val);
            }
            Assert.AreEqual(99, pq.Peek());
        }

        private int[] Range(int hi)
        {
            int[] a = new int[hi];
            for(int i=0; i < hi; ++i)
            {
                a[i] = i;
            }
            return a;
        }

        private void Print(int[] a)
        {
            foreach(int val in a)
            {
                Console.Write("{0} ", val);
            }
            Console.WriteLine();
        }

        private void Shuffle(int[] a)
        {
            int i = 0;
            Random random = new Random();
            while(i < a.Length)
            {
                int j = random.Next(i+1);
                Swap(a, i++, j);
            }
        }

        private void Swap(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
    }
}
