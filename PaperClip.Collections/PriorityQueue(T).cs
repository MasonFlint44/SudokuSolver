using PaperClip.Collections.Enums;
using PaperClip.Collections.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace PaperClip.Collections
{
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private readonly IPriorityQueue<T, T> _priorityQueue;

        public PriorityQueue(Priority priority, Stability stability = Stability.Unmanaged)
        {
            _priorityQueue = new PriorityQueue<T, T>(priority, stability);
        }

        public PriorityQueue(Priority priority, IComparer<T> comparer, Stability stability = Stability.Unmanaged)
        {
            _priorityQueue = new PriorityQueue<T, T>(priority, comparer, stability);
        }

        public PriorityQueue(Priority priority, IEnumerable<T> values, Stability stability = Stability.Unmanaged)
        {
            _priorityQueue = new PriorityQueue<T, T>(priority, values, value => value, stability);
        }

        public PriorityQueue(Priority priority, IEnumerable<T> values, IComparer<T> comparer, Stability stability = Stability.Unmanaged)
        {
            _priorityQueue = new PriorityQueue<T, T>(priority, values, value => value, comparer, stability);
        }

        public int Count => _priorityQueue.Count;

        public void AddRange(IEnumerable<T> values)
        {
            _priorityQueue.AddRange(values, value => value);
        }

        public void Clear()
        {
            _priorityQueue.Clear();
        }

        public T Dequeue()
        {
            return _priorityQueue.Dequeue();
        }

        public void Enqueue(T value)
        {
            _priorityQueue.Enqueue(value, value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _priorityQueue.GetEnumerator();
        }

        public T Peek()
        {
            return _priorityQueue.Peek();
        }

        public IEnumerable<T> Sort()
        {
            return _priorityQueue.Sort();
        }

        public IEnumerable<T> SortAscending()
        {
            return _priorityQueue.SortAscending();
        }

        public IEnumerable<T> SortDescending()
        {
            return _priorityQueue.SortDescending();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
