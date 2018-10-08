using System;
using System.Collections.Generic;
using PaperClip.Collections.Enums;

namespace PaperClip.Collections
{
    public class MinPrioritySet<TPriority, TKey, TValue>: PrioritySet<TPriority, TKey, TValue>
    {
        private const Priority PriorityOrder = Priority.Min;

        public TValue Min => Peek();

        public MinPrioritySet(Stability stability = Stability.Unmanaged) : base(PriorityOrder, stability) { }
        public MinPrioritySet(IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, comparer, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, stability) { }
        public MinPrioritySet(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc, IComparer<TPriority> comparer, Stability stability = Stability.Unmanaged) : base(PriorityOrder, values, keyFunc, priorityFunc, comparer, stability) { }
    }
}
