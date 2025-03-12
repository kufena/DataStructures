using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using DataStructures;

namespace DataStructuresTest
{
    class HeapSortTest
    {
        [SetUp]
        public void setup()
        {

        }

        public class mycomparer : IComparer<int>
        {
            public int Compare(int a, int b)
            {
                return a - b;
            }
        }

        [Test]
        public void shortTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[] { 1, 2, 3, 4 };
            int[] done = new int[] { 1, 2, 3, 4 };
            hs.Sort(arr, new mycomparer());
            for(int i = 0; i < arr.Length; i++)
            {
                ClassicAssert.AreEqual(arr[i], done[i]);
            }
        }

        [Test]
        public void emptyTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[] {};
            int[] done = new int[] {};
            hs.Sort(arr, new mycomparer());
            ClassicAssert.AreEqual(arr.Length, 0);
        }

        [Test]
        public void oneTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[] { 2 };
            int[] done = new int[] { 2 };
            hs.Sort(arr, new mycomparer());
            for (int i = 0; i < arr.Length; i++)
            {
                ClassicAssert.AreEqual(arr[i], done[i]);
            }
        }

        [Test]
        public void twoTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[] { 9, 2 };
            int[] done = new int[] { 2, 9 };
            hs.Sort(arr, new mycomparer());
            for (int i = 0; i < arr.Length; i++)
            {
                ClassicAssert.AreEqual(arr[i], done[i]);
            }
        }

        [Test]
        public void threeTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[] { 9, 2, 17 };
            int[] done = new int[] { 2, 9, 17 };
            hs.Sort(arr, new mycomparer());
            for (int i = 0; i < arr.Length; i++)
            {
                ClassicAssert.AreEqual(arr[i], done[i]);
            }
        }

        [Test]
        public void millionTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[1000000];
            int[] done = new int[1000000];
            for(int i = 0; i < 1000000; i++)
            {
                done[i] = i;
                arr[999999 - i] = i;
            }
            Random r = new Random();
            int x, y, z;
            for(int i = 0; i < 1000000; i++)
            {
                x = r.Next(1000000);
                y = r.Next(1000000);
                z = arr[x];
                arr[x] = arr[y];
                arr[y] = z;
            }
            hs.Sort(arr, new mycomparer());
            for (int i = 0; i < arr.Length; i++)
            {
                ClassicAssert.AreEqual(arr[i], done[i]);
            }
        }

        [Test]
        public void allTheSameTest()
        {
            HeapSort<int> hs = new HeapSort<int>();
            int[] arr = new int[1000000];
            int[] done = new int[1000000];
            for (int i = 0; i < 1000000; i++)
            {
                done[i] = 111;
                arr[i] = 111;
            }

            hs.Sort(arr, new mycomparer());
            for (int i = 0; i < arr.Length; i++)
            {
                ClassicAssert.AreEqual(arr[i], done[i]);
            }
        }

    }
}
 