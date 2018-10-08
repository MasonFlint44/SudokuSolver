namespace PaperClip.Collections
{
    public class PrioritySetElement<TPriority, TKey, TValue> : PriorityQueueElement<TPriority, TValue>
    {
        public TKey Key { get; internal set; }

        public PrioritySetElement(TPriority priority, TKey key, TValue value) : base(priority, value)
        {
            Key = key;
        }
    }
}
