using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using DataStructures;

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

            int count = 0;
            int start = 0;
            int step = 37;
            while(count < 1000)
            {
                tree.insert(start, 1000 - start);
                start = (start + step) % 1000;
                count++;
            }

            Assert.Pass();
        }
    }
}
