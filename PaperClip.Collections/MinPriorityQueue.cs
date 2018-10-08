using PaperClip.Collections.Enums;
using System;
using System.Collections.Generic;

namespace PaperClip.Collections
{
    public class MinPriorityQueue<TPriority, TValue> : PriorityQueue<TPriority, TValue>
    {
        private const Priority PriorityOrder = Priority.Min;

        public TValue Min => Peek();

        public MinPriorityQueue(Stability stability = Stability.Unmanaged) : base(PriorityOrder, stability) { }
        public MinPriorityQueue(IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, comparer, stability) { }
        public MinPriorityQueue(IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, stability) { }
        public MinPriorityQueue(IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, stability) { }
        public MinPriorityQueue(IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, comparer, stability) { }
        public MinPriorityQueue(IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, priorityFunc, comparer, stability) { }
    }
}
