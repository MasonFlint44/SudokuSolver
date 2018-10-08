using PaperClip.Collections.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using PaperClip.Collections.Enums;

namespace PaperClip.Collections
{
    public class PrioritySet<TPriority, TKey, TValue> : IPrioritySet<TPriority, TKey, TValue>
    {
        private readonly Dictionary<TKey, PrioritySetElement<TPriority, TKey, TValue>> _elements =
            new Dictionary<TKey, PrioritySetElement<TPriority, TKey, TValue>>();

        private readonly PriorityQueue<TPriority, TValue> _priorityQueue;

        public PrioritySet(Priority priority, Stability stability = Stability.Unmanaged)
        {
            _priorityQueue = new PriorityQueue<TPriority, TValue>(priority, stability);
        }

        public PrioritySet(Priority priority, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged)
        {
            _priorityQueue = new PriorityQueue<TPriority, TValue>(priority, comparer, stability);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : this(priority, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : this(priority, comparer, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : this(priority, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : this(priority, comparer, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : this(priority, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : this(priority, comparer, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : this(priority, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public PrioritySet(Priority priority, IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : this(priority, comparer, stability)
        {
            AddRange(values, keyFunc, priorityFunc);
        }

        public int Count => _priorityQueue.Count;

        public bool Contains(TKey key)
        {
            return _elements.ContainsKey(key);
        }

        public bool UpdatePriority(TKey key, TPriority priority)
        {
            if (!_elements.TryGetValue(key, out var element)) return false;

            element.Priority = priority;

            if (_priorityQueue.BubbleUp(element.Index) == false)
            {
                _priorityQueue.Heapify(element.Index);
            }
            return true;
        }

        public bool UpdatePriority(TKey key, Func<TPriority, TPriority> priorityFunc)
        {
            if (!_elements.TryGetValue(key, out var element)) return false;

            element.Priority = priorityFunc(element.Priority);

            if (_priorityQueue.BubbleUp(element.Index) == false)
            {
                _priorityQueue.Heapify(element.Index);
            }
            return true;
        }

        public bool UpdateValue(TKey key, TValue value)
        {
            if (!_elements.TryGetValue(key, out var element)) return false;
            element.Value = value;
            return true;
        }

        public bool UpdateValue(TKey key, Func<TValue, TValue> valueFunc)
        {
            if (!_elements.TryGetValue(key, out var element)) return false;
            element.Value = valueFunc(element.Value);
            return true;
        }

        public bool UpdateKey(TKey oldKey, TKey newKey)
        {
            if (!_elements.TryGetValue(oldKey, out var element)) return false;

            element.Key = newKey;

            _elements.Remove(oldKey);
            _elements.Add(newKey, element);
            return true;
        }

        public bool Remove(TKey key)
        {
            if (!_elements.TryGetValue(key, out var element)) return false;

            _priorityQueue.RemoveIndexFromHeap(element.Index);
            _priorityQueue.Heapify(element.Index);
            _elements.Remove(key);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            if (!_elements.TryGetValue(key, out var element)) { return false; }
            value = element.Value;
            return true;
        }

        public bool TryGetPriority(TKey key, out TPriority priority)
        {
            priority = default(TPriority);
            if (!_elements.TryGetValue(key, out var element)) { return false; }
            priority = element.Priority;
            return true;
        }

        public void AddRange(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc)
        {
            // Cast to list to avoid multiple enumerations
            var list = values as IList<TValue> ?? values.ToList();

            var keys = list.Select(keyFunc);
            var priorities = list.Select(priorityFunc);
            AddRange(list, keys, priorities);
        }

        public void AddRange(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc)
        {
            // Cast to list to avoid multiple enumerations
            var list = values as IList<TValue> ?? values.ToList();

            var keys = list.Select(keyFunc);
            var priorities = list.Select(priorityFunc);
            AddRange(list, keys, priorities);
        }

        public void AddRange(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc)
        {
            // Cast to list to avoid multiple enumerations
            var list = values as IList<TValue> ?? values.ToList();

            var keys = list.Select(keyFunc);
            var priorities = list.Select(priorityFunc);
            AddRange(list, keys, priorities);
        }

        public void AddRange(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc)
        {
            // Cast to list to avoid multiple enumerations
            var list = values as IList<TValue> ?? values.ToList();

            var keys = list.Select(keyFunc);
            var priorities = list.Select(priorityFunc);
            AddRange(list, keys, priorities);
        }

        private void AddRange(IEnumerable<TValue> values, IEnumerable<TKey> keys, IEnumerable<TPriority> priorities)
        {
            var elements = values.Select((value, index) =>
                new PrioritySetElement<TPriority, TKey, TValue>(priorities.ElementAt(index), keys.ElementAt(index),
                    value)).ToList();

            for (var i = 0; i < elements.Count; i++)
            {
                var element = elements.ElementAt(i);
                _elements.Add(keys.ElementAt(i), element);
            }
            _priorityQueue.AddRange(elements);
        }

        public void Clear()
        {
            _elements.Clear();
            _priorityQueue.Clear();
        }

        public TValue Dequeue()
        {
            var element = (PrioritySetElement<TPriority, TKey, TValue>)_priorityQueue.DequeueElement();
            if(element == null) { return default(TValue); }

            _elements.Remove(element.Key);
            return element.Value;
        }

        public void Enqueue(TPriority priority, TKey key, TValue value)
        {
            var element = new PrioritySetElement<TPriority, TKey, TValue>(priority, key, value);
            _elements.Add(key, element);
            _priorityQueue.Enqueue(element);
        }

        public TValue Peek()
        {
            return _priorityQueue.Peek();
        }

        public IEnumerable<TValue> Sort()
        {
            return _priorityQueue.Sort();
        }

        public IEnumerable<TValue> SortAscending()
        {
            return _priorityQueue.SortAscending();
        }

        public IEnumerable<TValue> SortDescending()
        {
            return _priorityQueue.SortDescending();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _priorityQueue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
