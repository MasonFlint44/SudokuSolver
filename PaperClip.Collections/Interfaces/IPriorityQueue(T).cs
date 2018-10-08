using System.Collections.Generic;

namespace PaperClip.Collections.Interfaces
{
    public interface IPriorityQueue<T> : IEnumerable<T>
    {
        int Count { get; }

        IEnumerable<T> Sort();
        IEnumerable<T> SortAscending();
        IEnumerable<T> SortDescending();
        void AddRange(IEnumerable<T> values);
        void Clear();
        T Peek();
        T Dequeue();
        void Enqueue(T item);
    }
}
