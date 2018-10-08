using PaperClip.Collections.Enums;
using System;
using System.Collections.Generic;

namespace PaperClip.Collections
{
    public class MaxPriorityQueue<TPriority, TValue> : PriorityQueue<TPriority, TValue>
    {
        private const Priority PriorityOrder = Priority.Max;

        public TValue Max => Peek();

        public MaxPriorityQueue(Stability stability = Stability.Unmanaged) : base(PriorityOrder, stability) { }
        public MaxPriorityQueue(IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, comparer, stability) { }
        public MaxPriorityQueue(IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, stability) { }
        public MaxPriorityQueue(IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, stability) { }
        public MaxPriorityQueue(IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, comparer, stability) { }
        public MaxPriorityQueue(IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, comparer, stability) { }
    }
}
