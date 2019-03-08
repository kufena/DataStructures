using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DataStructures
{
    /**
     * Red/Black tree implementation more or less lifted from wikipedia.
     * Not particularly OO at the moment.
     */
    public class RedBlackTree<K, V> where K : IComparable
    {

        public enum Colour { red, black }

        Node root = null; // new Node { leaf = true, colour = Colour.black };
        static Node LEAF = new Node { leaf = true, colour = Colour.black };

        Stopwatch sp;

        public RedBlackTree()
        {
            sp = new Stopwatch();
            insert_accum = 0;
            repair_accum = 0;
        }

        long insert_accum;
        long repair_accum;

        public Option<V> search(K key)
        {
            Node x = root;

            while (!x.leaf)
            {
                if (x.key.CompareTo(key) == 0)
                    return Option<V>.Some(x.value);
                else
                {
                    if (x.key.CompareTo(key) > 0)
                        x = x.left;
                    else
                        x = x.right;
                }
            }

            return Option<V>.None();
        }

        public void insert(K key, V value)
        {
            var nnew = new Node
            {
                colour = Colour.black,
                key = key,
                value = value,
                left = null,
                right = null,
                parent = null,
                leaf = false
            };

            /*
            if (root == null)
            {
                root = nnew;
                return;
            }
            */

            root = insert(root, nnew);
        }

        private Node insert(Node root, Node n)
        {
            sp.Reset();
            sp.Start();
            //insert_recurse(root, n);
            insert_loop(root, n);
            sp.Stop();
            insert_accum += sp.ElapsedTicks;

            sp.Reset();
            sp.Start();
            insert_repair(n);
            sp.Stop();
            repair_accum += sp.ElapsedTicks;

            // we're returning a new root - we better find it first.
            var nroot = n;
            while (nroot.parent != null)
                nroot = nroot.parent;
            return nroot;
        }

        long insrec_search;
        long insrec_rest;

        private void insert_recurse(Node root, Node n)
        {
            if (root != null && n.key.CompareTo(root.key) <= 0)
            {
                if (!root.left.leaf) {
                    insert_recurse(root.left, n);
                    return;
                }
                else
                    root.left = n;
            }
            else if (root != null)
            {
                if (!root.right.leaf) {
                    insert_recurse(root.right, n);
                    return;
                }
                else
                    root.right = n;
            }

            n.parent = root;
            n.left = LEAF; // new Node { leaf = true, colour = Colour.black };
            n.right = LEAF; // new Node { leaf = true, colour = Colour.black };
            n.colour = Colour.red;
        }

        private void insert_loop(Node root, Node n)
        {
            while (root != null)
            {
                if (n.key.CompareTo(root.key) <= 0)
                {
                    if (!root.left.leaf)
                    {
                        root = root.left;
                    }
                    else
                    {
                        root.left = n;
                        break;
                    }
                }
                else
                {
                    if (!root.right.leaf)
                    {
                        root = root.right;

                    }
                    else
                    {
                        root.right = n;
                        break;
                    }
                }
            }

            n.parent = root;
            n.left = LEAF; // new Node { leaf = true, colour = Colour.black };
            n.right = LEAF; // new Node { leaf = true, colour = Colour.black };
            n.colour = Colour.red;
        }

        private void insert_repair(Node n)
        {
            if (n.parent == null)
                n.colour = Colour.black;
            else if (n.parent.colour == Colour.black)
                return;
            else if (n.uncle() != null && n.uncle().colour == Colour.red) // how do we know uncle isn't null?  because parent is red and not null!
            {
                n.parent.colour = Colour.black;
                n.uncle().colour = Colour.black;
                n.grandparent().colour = Colour.red;
                insert_repair(n.grandparent());
            }
            else {
                var p = n.parent;
                var g = n.grandparent();
                if (n == g.left.right)
                {
                    p.rotate_left();
                    n = n.left;
                }
                else if (n == g.right.left)
                {
                    p.rotate_right();
                    n = n.right;
                }

                insert_case4_step2(n);
            }
        }

        private void insert_case4_step2(Node n)
        {
            var p = n.parent;
            var g = n.grandparent();
            if (n == p.left)
                g.rotate_right();
            else
                g.rotate_left();
            p.colour = Colour.black;
            g.colour = Colour.red;
        }

        protected class Node
        {
            public bool leaf { get; set; }
            public K key { get; set; }
            public V value { get; set; }

            public Colour colour { get; set; }

            public Node left { get; set; }
            public Node right { get; set; }
            public Node parent { get; set; }

            public Node grandparent()
            {
                if (parent == null)
                    return null;
                else
                    return parent.parent;
            }

            public Node sibling()
            {
                if (parent == null)
                    return null;
                else
                {
                    if (parent.right == this)
                        return parent.left;
                    else
                        return parent.right;
                }
            }

            public Node uncle()
            {
                Node gp = grandparent();
                if (gp == null)
                    return null;

                return parent.sibling();
            }

            public Node rotate_left()
            {
                var nnew = right;
                var pare = parent;
                if (nnew.leaf) // can't do this.
                    throw new Exception("rotate left on node with null right branch.");

                right = nnew.left;
                nnew.left = this;
                parent = nnew;

                if (right != null)
                    right.parent = this;
                if (pare != null)
                {
                    if (this == pare.left)
                        pare.left = nnew;
                    else
                        pare.right = nnew;
                }
                nnew.parent = pare;
                return nnew;
            }

            public Node rotate_right()
            {
                var nnew = left;
                var pare = parent;
                if (nnew.leaf)
                    throw new Exception("rotate right on node with null left branch");
                left = nnew.right;
                nnew.right = this;
                this.parent = nnew;
                if (left != null)
                    left.parent = this;
                if (pare != null)
                {
                    if (this == pare.left)
                        pare.left = nnew;
                    else
                        pare.right = nnew;
                }
                nnew.parent = pare;
                return nnew;
            }
        }
    }
}
