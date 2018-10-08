using System;
using System.Collections.Generic;
using PaperClip.Collections.Enums;

namespace PaperClip.Collections
{
    public class MaxPrioritySet<TPriority, TKey, TValue> : PrioritySet<TPriority, TKey, TValue>
    {
        private const Priority PriorityOrder = Priority.Max;

        public TValue Max => Peek();

        public MaxPrioritySet(Stability stability = Stability.Unmanaged) : base(PriorityOrder, stability) { }
        public MaxPrioritySet(IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, comparer, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MaxPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
    }
}
