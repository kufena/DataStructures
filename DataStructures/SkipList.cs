using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /*
     * Implementation taken from A Skip List Cookbook, Pugh W, UMIACS-TR-89-72.1, Univeristy of Maryland, 1989.
     * 
     * I'm not happy about the treatment of HEAD and NIL here. For a start, should HEAD have all the levels?  I think
     * it probably should.  Secondly, relies on a min and max for the key type K but we don't have one, so I ask
     * for these in the constructor.  This doesn't seem right, but allows us to not have to keep checking for NIL
     * and HEAD.
     */
    public class SkipList<K,V> where K : IComparable<K>
    {

        Random Rand;
        int MaxLevel;

        SkipNode HEAD;
        SkipNode NIL;

        public SkipList(K min, K max)
        {
            Rand = new Random();
            setUp(max, min);
        }

        public SkipList(K min, K max, int seed)
        {
            Rand = new Random(seed);
            setUp(max, min);
        }

        private void setUp(K max, K min)
        {
            MaxLevel = 10;
            HEAD = new SkipNode(min, default(V), MaxLevel - 1, MaxLevel);
            NIL = new SkipNode(max, default(V), 0, MaxLevel);
            for(int i = 0; i<MaxLevel; i++) HEAD.setLevel(NIL, i);
        }

        int newLevel()
        {
            double step = Rand.NextDouble();
            int level = 0;
            while ((step = Rand.NextDouble()) > 0.5 && level < (MaxLevel-1))
            {
                level++;
            }
            return level;
        }

        public Option<V> search(K key)
        {
            SkipNode x = HEAD;
            for (int i = MaxLevel-1; i >= 0; i--)
            {
                while (x.levels[i].key.CompareTo(key) < 0)
                {
                    x = x.levels[i];
                }
            }
            if (x.levels[0].key.CompareTo(key) == 0)
                return Option<V>.Some(x.levels[0].value);
            else
                return Option<V>.None(); // default(V);
        }

        public V insert(K key, V value)
        {
            SkipNode[] update = new SkipNode[MaxLevel];
            var x = HEAD;
            for (int i = MaxLevel - 1; i >= 0; i--)
            {
                while (x.levels[i].key.CompareTo(key) < 0)
                {
                    x = x.levels[i];
                }
                update[i] = x;
            }
            x = x.levels[0];
            if (x.key.CompareTo(key) == 0)
            {
                V old = x.value;
                x.value = value;
                return old;
            }
            else
            {
                var newlevel = newLevel();
                var newnode = new SkipNode(key, value, newlevel, MaxLevel);
                for (int i = 0; i <= newlevel; i++)
                {
                    newnode.levels[i] = update[i].levels[i];
                    update[i].levels[i] = newnode;
                }
            }
            return value;
        }

        class SkipNode
        {
            public K key { get; set; }
            public V value { get; set; }
            int level { get; set; }

            public SkipNode[] levels;

            public SkipNode(K k, V v, int l, int maxl)
            {
                key = k;
                value = v;
                level = l;
                levels = new SkipNode[maxl];
            }

            public void setLevel(SkipNode sn, int i)
            {
                levels[i] = sn;
            }

        }
    }
}
