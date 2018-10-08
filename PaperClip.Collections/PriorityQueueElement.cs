namespace PaperClip.Collections
{
    public class PriorityQueueElement<TPriority, TValue>
    {
        internal int InsertionIndex { get; set; }
        
        public TPriority Priority { get; internal set; }

        public TValue Value { get; internal set; }

        internal int Index { get; set; }

        public PriorityQueueElement(TPriority priority, TValue value)
        {
            Priority = priority;
            Value = value;
        }
    }
}
