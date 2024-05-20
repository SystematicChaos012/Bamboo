using SharedKernel.Domain.Accessors;
using SharedKernel.Sequences;
using System.Collections;

namespace SharedKernel.Domain.Collections
{
    /// <summary>
    /// 追踪实体集合
    /// </summary>
    internal sealed class TrackCollection<T>(SequentialValue sequentialValue) : ICollection<T> where T : Entity
    {
        private readonly List<T> _value = new();

        public int Count => _value.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            EntityUnsafeAccessor.SequentialValue(item) = sequentialValue;

            foreach (var domainEvent in EntityUnsafeAccessor.DomainEvents(item))
            {
                DomainEventUnsafeAccessor.Seq(domainEvent) = sequentialValue.Next();
            }

            _value.Add(item);
        }

        public void Clear() => _value.Clear();

        public bool Contains(T item) => _value.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _value.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _value.GetEnumerator();

        public bool Remove(T item) => _value.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
