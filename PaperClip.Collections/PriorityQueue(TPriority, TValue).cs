using PaperClip.Collections.Enums;
using PaperClip.Collections.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PaperClip.Collections
{
    public class PriorityQueue<TPriority, TValue> : IPriorityQueue<TPriority, TValue>
    {
        private readonly IList<PriorityQueueElement<TPriority, TValue>> _heap = new List<PriorityQueueElement<TPriority, TValue>>();
        private readonly IComparer<TPriority> _comparer;
        private readonly Priority _priority;
        private readonly Stability _stability;
        private int _insertionIndex;

        public int Count => _heap.Count;

        public PriorityQueue(Priority priority, Stability stability = Stability.Unmanaged) : this(priority, Comparer<TPriority>.Default, stability) { }

        public PriorityQueue(Priority priority, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged)
        {
            _priority = priority;
            _stability = stability;
            _comparer = comparer;
        }

        public PriorityQueue(Priority priority, IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : this(priority, stability)
        {
            AddRange(values, priorityFunc);
        }

        public PriorityQueue(Priority priority, IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : this(priority, stability)
        {
            AddRange(values, priorityFunc);
        }

        public PriorityQueue(Priority priority, IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : this(priority, comparer, stability)
        {
            AddRange(values, priorityFunc);
        }

        public PriorityQueue(Priority priority, IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : this(priority, comparer, stability)
        {
            AddRange(values, priorityFunc);
        }

        public IEnumerable<TValue> Sort()
        {
            var list = new List<PriorityQueueElement<TPriority, TValue>>(_heap);
            for (var i = _heap.Count - 1; i >= 1; i--)
            {
                Swap(list, i, 0);
                Heapify(list, i, 0);
            }
            return list.Select(x => x.Value).ToList();
        }

        public IEnumerable<TValue> SortAscending()
        {
            var sort = Sort();
            if (_priority == Priority.Min)
            {
                sort = sort.Reverse();
            }
            return sort;
        }

        public IEnumerable<TValue> SortDescending()
        {
            var sort = Sort();
            if (_priority == Priority.Max)
            {
                sort = sort.Reverse();
            }
            return sort;
        }

        public void AddRange(IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc)
        {
            AddRange(values.Select((value, index) => new PriorityQueueElement<TPriority, TValue>(priorityFunc(value), value)));
        }

        public void AddRange(IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc)
        {
            AddRange(values.Select((value, index) => new PriorityQueueElement<TPriority, TValue>(priorityFunc(value, index), value)));
        }

        internal void AddRange(IEnumerable<PriorityQueueElement<TPriority, TValue>> values)
        {
            foreach (var value in values)
            {
                value.InsertionIndex = _insertionIndex++;
                value.Index = _heap.Count;
                _heap.Add(value);
            }
            for (var i = (_heap.Count / 2) - 1; i >= 0; i--)
            {
                Heapify(_heap, _heap.Count, i);
            }
        }

        public void Clear()
        {
            _heap.Clear();
        }

        private TValue Peek(int index)
        {
            return _heap[index].Value;
        }

        public TValue Peek()
        {
            if(Count == 0) { return default(TValue); }
            return Peek(0);
        }

        public TValue Dequeue()
        {
            PriorityQueueElement<TPriority, TValue> element;
            if((element = Dequeue(_heap)) == null) { return default(TValue); }
            return element.Value;
        }

        internal PriorityQueueElement<TPriority, TValue> DequeueElement()
        {
            return Dequeue(_heap);
        }

        private PriorityQueueElement<TPriority, TValue> Dequeue(IList<PriorityQueueElement<TPriority, TValue>> heap)
        {
            if(Count == 0) { return null; }

            Swap(heap, heap.Count - 1, 0);
            var min = RemoveIndex(heap, heap.Count - 1);
            Heapify(heap, heap.Count, 0);

            return min;
        }

        public void Enqueue(TPriority priority, TValue value)
        {
            Enqueue(new PriorityQueueElement<TPriority, TValue>(priority, value));
        }

        internal void Enqueue(PriorityQueueElement<TPriority, TValue> value)
        {
            value.InsertionIndex = _insertionIndex++;
            value.Index = _heap.Count;
            _heap.Add(value);

            BubbleUp(value.Index);
        }

        internal bool BubbleUp(int index)
        {
            var value = _heap[index];
            var bubbled = false;
            while (BubbleUpPriorityFilter(value, index))
            {
                var parent = GetParentIndex(index);
                Swap(_heap, index, parent);
                index = parent;
                bubbled = true;
            }
            return bubbled;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private bool BubbleUpPriorityFilter(PriorityQueueElement<TPriority, TValue> value, int index)
        {
            if (index <= 0) return false;

            var parent = GetParentIndex(index);
            var comparison = _comparer.Compare(_heap[parent].Priority, value.Priority);
            if (comparison == -1 * (int)_priority)
            {
                return true;
            }
            // Resolve ties based on queue order
            if (_stability == Stability.Unmanaged) { return false; }
            return comparison == 0 && _heap[parent].InsertionIndex.CompareTo(value.InsertionIndex) == -1 * (int)_stability;
        }

        private PriorityQueueElement<TPriority, TValue> RemoveIndex(IList<PriorityQueueElement<TPriority, TValue>> list, int index)
        {
            var element = list[index];
            list.RemoveAt(index);
            for (var i = index; i < list.Count; i++)
            {
                list[i].Index--;
            }
            return element;
        }

        internal PriorityQueueElement<TPriority, TValue> RemoveIndexFromHeap(int index)
        {
            return RemoveIndex(_heap, index);
        }

        private void Swap(IList<PriorityQueueElement<TPriority, TValue>> list, int x, int y)
        {
            // Swap elements
            var swap = list[x];
            list[x] = list[y];
            list[y] = swap;

            // Swap indexes on elements
            var swapIndex = list[x].Index;
            list[x].Index = list[y].Index;
            list[y].Index = swapIndex;
        }

        private bool HeapifyPriorityFilter(IList<PriorityQueueElement<TPriority, TValue>> list, int index, int child, int size)
        {
            if (child > size - 1) return false;

            var comparison = _comparer.Compare(list[child].Priority, list[index].Priority);
            if (comparison == (int)_priority)
            {
                return true;
            }
            // Resolve ties based on queue order
            if (_stability == Stability.Unmanaged) return false;
            return comparison == 0 && list[child].InsertionIndex.CompareTo(list[index].InsertionIndex) == (int)_stability;
        }

        internal void Heapify(int index)
        {
            Heapify(_heap, _heap.Count, index);
        }

        private void Heapify(IList<PriorityQueueElement<TPriority, TValue>> list, int size, int index)
        {
            var extreme = index;
            var right = 2 * (index + 1);
            var left = right - 1;

            if (HeapifyPriorityFilter(list, extreme, left, size))
            {
                extreme = left;
            }
            if (HeapifyPriorityFilter(list, extreme, right, size))
            {
                extreme = right;
            }
            if (extreme != index)
            {
                Swap(list, extreme, index);
                Heapify(list, size, extreme);
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            var heap = new List<PriorityQueueElement<TPriority, TValue>>(_heap);
            for(var i = 0; i < heap.Count; i++)
            {
                yield return Dequeue(heap).Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
