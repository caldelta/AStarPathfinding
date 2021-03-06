using System;
using UnityEngine;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// Priority implement from https://gist.github.com/paralleltree/31045ab26f69b956052c
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> list;
        public int Count { get { return list.Count; } }
        public readonly bool IsDescending;

        public PriorityQueue()
        {
            list = new List<T>();
        }

        public PriorityQueue(bool isdesc)
            : this()
        {
            IsDescending = isdesc;
        }

        public PriorityQueue(int capacity)
            : this(capacity, false)
        { }

        public PriorityQueue(IEnumerable<T> collection)
            : this(collection, false)
        { }

        public PriorityQueue(int capacity, bool isdesc)
        {
            list = new List<T>(capacity);
            IsDescending = isdesc;
        }

        public PriorityQueue(IEnumerable<T> collection, bool isdesc)
            : this()
        {
            IsDescending = isdesc;
            foreach (var item in collection)
                Enqueue(item);
        }


        public void Enqueue(T x)
        {
            list.Add(x);
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if ((IsDescending ? -1 : 1) * list[p].CompareTo(x) <= 0) break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i] = x;
        }

        public T Dequeue()
        {
            T target = Peek();
            T root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && (IsDescending ? -1 : 1) * list[b].CompareTo(list[a]) < 0 ? b : a;

                if ((IsDescending ? -1 : 1) * list[c].CompareTo(root) >= 0) break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return target;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T x)
        {
            if (Count == 0)
                return false;

            var min = 0;
            var max = Count - 1;

            int guessIndex = (int)Mathf.Floor(min + max) / 2;
            var guess = list[guessIndex];
            while (min != max)
            {
                if (min > Count)
                    break;
                if (max < 0)
                    break;

                if (guess.CompareTo(x) < 0)
                {
                    min = guessIndex + 1;
                }
                if (guess.CompareTo(x) > 0)
                {
                    max = guessIndex - 1;
                }

                guessIndex = (int)Mathf.Floor(min + max) / 2;
                guess = list[guessIndex];

                if (guess.CompareTo(x) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<T> getList()
        {
            return list;
        }
    }
}