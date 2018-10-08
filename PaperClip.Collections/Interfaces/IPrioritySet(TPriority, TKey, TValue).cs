using System;
using System.Collections.Generic;

namespace PaperClip.Collections.Interfaces
{
    public interface IPrioritySet<TPriority, in TKey, TValue> : IEnumerable<TValue>
    {
        int Count { get; }

        bool Contains(TKey key);
        bool UpdatePriority(TKey key, TPriority priority);
        bool UpdatePriority(TKey key, Func<TPriority, TPriority> priorityFunc);
        bool UpdateValue(TKey key, TValue value);
        bool UpdateValue(TKey key, Func<TValue, TValue> valueFunc);
        bool UpdateKey(TKey oldKey, TKey newKey);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        bool TryGetPriority(TKey key, out TPriority priority);
        void AddRange(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, TPriority> priorityFunc);
        void AddRange(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc);
        void AddRange(IEnumerable<TValue> values, Func<TValue, int, TKey> keyFunc, Func<TValue, TPriority> priorityFunc);
        void AddRange(IEnumerable<TValue> values, Func<TValue, TKey> keyFunc, Func<TValue, int, TPriority> priorityFunc);
        void Clear();
        TValue Dequeue();
        void Enqueue(TPriority priority, TKey key, TValue value);
        TValue Peek();
        IEnumerable<TValue> Sort();
        IEnumerable<TValue> SortAscending();
        IEnumerable<TValue> SortDescending();
    }
}
