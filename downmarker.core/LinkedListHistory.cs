using System;
using System.Collections.Generic;

namespace DownMarker.Core
{
    /// <summary>
    /// An implementation of <see cref="IHistory"/> which simply
    /// stores all states (or state changes) in a linked list.
    /// </summary>
    /// <typeparam name="T">The type of state or state change to track.</typeparam>
    public class LinkedListHistory<T> : IHistory<T>
    {
        private readonly int limit;
        private readonly LinkedList<T> history = new LinkedList<T>();
        private LinkedListNode<T> current;

        public LinkedListHistory(int limit)
        {
            if (limit < 1)
                throw new ArgumentOutOfRangeException("limit", "Limit must be at least 1");
            this.limit = limit;
        }

        public bool CanGoBack
        {
            get
            {
                return (current != null) && (current.Previous != null);
            }
        }

        public bool CanGoForward
        {
            get
            {
                return (current != null) && (current.Next != null)
                   && (current.Next.Value != null);
            }
        }

        public T Current
        {
            get
            {
                if (this.current == null)
                {
                    return default(T);
                }
                else
                {
                    return this.current.Value;
                }
            }
        }

        public void Add(T value)
        {
            if (current == null)
            {
                // initialize linked list with first node
                current = history.AddFirst(value);
            }
            else if (current.Value == null)
            {
               // null values are allowed, but considered dummy nodes which
               // are replaced at the next invocation of Add
               current.Value = value;
            }
            else
            {
               current = history.AddAfter(current, value);
               // throw away redo history which is no longer applicable
               while (current.Next != null)
               {
                  history.Remove(current.Next);
               }
               // trim history to size limit
               while (history.Count > limit)
               {
                  history.Remove(history.First);
               }
            }
        }

        public T Back()
        {
            if (!CanGoBack)
                throw new InvalidOperationException();
            current = current.Previous;
            return current.Value;
        }

        public T Forward()
        {
            if (!CanGoForward)
                throw new InvalidOperationException();
            current = current.Next;
            return current.Value;
        }

        public void Clear()
        {
            history.Clear();
            current = null;
        }
    }
}
