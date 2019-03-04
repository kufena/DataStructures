using System;
using System.Collections.Generic;
using System.Text;

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

        public RedBlackTree()
        {
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
            insert_recurse(root, n);
            insert_repair(n);

            // we're returning a new root - we better find it first.
            var nroot = n;
            while (nroot.parent != null)
                nroot = nroot.parent;
            return nroot;
        }

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
            n.left = new Node { leaf = true, colour = Colour.black };
            n.right = new Node { leaf = true, colour = Colour.black };
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
