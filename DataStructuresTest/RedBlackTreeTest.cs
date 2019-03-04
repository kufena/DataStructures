using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using DataStructures;
using System.Diagnostics;

namespace DataStructuresTest
{
    /**
     * Tests for red/black trees.
     */
    class RedBlackTreeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void insert_one()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            tree.insert(1, 100);
            Assert.Pass();
        }

        [Test]
        public void insert_two()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            tree.insert(1, 100);
            tree.insert(2, 99);
            Assert.Pass();
        }

        [Test]
        public void insert_three()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            tree.insert(1, 100);
            tree.insert(2, 99);
            tree.insert(3, 98);
            Assert.Pass();
        }

        [Test]
        public void insert_ten_in_order()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            tree.insert(1, 100);
            tree.insert(2, 99);
            tree.insert(3, 98);
            tree.insert(4, 97);
            tree.insert(5, 96);
            tree.insert(6, 95);
            tree.insert(7, 94);
            tree.insert(8, 93);
            tree.insert(9, 92);
            tree.insert(10, 91);

            Assert.Pass();
        }

        [Test]
        public void insert_ten_out_of_order()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();

            tree.insert(4, 97);
            tree.insert(5, 96);
            tree.insert(1, 100);
            tree.insert(3, 98);
            tree.insert(10, 91);
            tree.insert(6, 95);
            tree.insert(8, 93);
            tree.insert(9, 92);
            tree.insert(2, 99);
            tree.insert(7, 94);

            Assert.Pass();
        }

        [Test]
        public void insert_1000_out_of_order()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();

            Stopwatch sp = new Stopwatch();

            int amount = 10000000;
            int count = 0;
            int start = 0;
            int step = 37;

            int[] keys = new int[amount];
            int[] vals = new int[amount];
            while (count < amount)
            {
                keys[count] = start = (start + step) % amount;
                vals[count] = amount - keys[count];
                count++;
            }

            count = 0;
            sp.Start();
            for (count = 0; count < amount; count++)
            {
                tree.insert(keys[count], vals[count]);
            }
            sp.Stop();
            long rbinterval = sp.ElapsedMilliseconds;

            sp.Reset();

            sp.Start();
            for (count = 0; count < amount; count++)
            {
                dict.Add(keys[count],vals[count]);
            }
            sp.Stop();
            long dictinterval = sp.ElapsedMilliseconds;

            Assert.Pass();
        }
    }
}
