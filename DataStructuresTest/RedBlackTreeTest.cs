using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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
            (bool missing, bool equal) = test_for_value(1, 100, tree);
            ClassicAssert.AreEqual(true, !missing);
            ClassicAssert.AreEqual(true, equal);
        }

        [Test]
        public void insert_two()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            tree.insert(1, 100);
            tree.insert(2, 99);


            (bool missing, bool equal) = test_for_value(1, 100, tree);
            ClassicAssert.AreEqual(true, !missing);  
            ClassicAssert.AreEqual(true,equal);     


            (missing, equal) = test_for_value(2, 99, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

        }

        [Test]
        public void insert_three()
        {
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            tree.insert(1, 100);
            tree.insert(2, 99);
            tree.insert(3, 98);

            (bool missing, bool equal) = test_for_value(1, 100, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);


            (missing, equal) = test_for_value(2, 99, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(3, 98, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);
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

            (bool missing, bool equal) = test_for_value(1, 100, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);


            (missing, equal) = test_for_value(2, 99, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(3, 98, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(4, 97, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(5, 96, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(6, 95, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(7, 94, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(8, 93, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(9, 92, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(10, 91, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);
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

            (bool missing, bool equal) = test_for_value(1, 100, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);


            (missing, equal) = test_for_value(2, 99, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(3, 98, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(4, 97, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(5, 96, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(6, 95, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(7, 94, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(8, 93, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(9, 92, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);

            (missing, equal) = test_for_value(10, 91, tree);
            ClassicAssert.AreEqual(true,!missing);
            ClassicAssert.AreEqual(true,equal);
        }

        [Test]
        public void insert_1000_out_of_order()
        {
            Stopwatch sp = new Stopwatch();

            sp.Start();
            RedBlackTree<int, int> tree = new RedBlackTree<int, int>();
            //Dictionary<int, int> dict = new Dictionary<int, int>();
            sp.Stop();

            long rbtreecreate = sp.ElapsedMilliseconds;
            sp.Reset();

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

            /*
            sp.Start();
            for (count = 0; count < amount; count++)
            {
                dict.Add(keys[count],vals[count]);
            }
            sp.Stop();
            long dictinterval = sp.ElapsedMilliseconds;
            */

            for (count = amount - 1; count >= 0; count--)
            {
                bool missing = true;
                var opt = tree.search(keys[count]);
                foreach(var op in opt)
                {
                    missing = false;
                    ClassicAssert.AreEqual(op, vals[count]);
                }
                ClassicAssert.AreEqual(true,!missing);
            }
        }

        public (bool, bool) test_for_value(int key, int val, RedBlackTree<int, int> tree)
        {
            var opt = tree.search(key);
            bool missing = true;
            bool equal = false;
            foreach (var v in opt)
            {
                equal = (v == val);
                missing = false;
            }
            return (missing, equal);
        }
    }
}
