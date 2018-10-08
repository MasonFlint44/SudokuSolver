using System;
using System.Collections.Generic;

namespace PaperClip.Collections.Interfaces
{
    public interface IPriorityQueue<in TPriority, TValue> : IEnumerable<TValue>
    {
        int Count { get; }

        IEnumerable<TValue> Sort();
        IEnumerable<TValue> SortAscending();
        IEnumerable<TValue> SortDescending();
        void AddRange(IEnumerable<TValue> values, Func<TValue, TPriority> priorityFunc);
        void AddRange(IEnumerable<TValue> values, Func<TValue, int, TPriority> priorityFunc);
        void Clear();
        TValue Peek();
        TValue Dequeue();
        void Enqueue(TPriority priority, TValue value);
    }
}
