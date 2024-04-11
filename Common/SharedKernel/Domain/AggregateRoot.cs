
namespace SharedKernel.Domain
{
    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class AggregateRoot<TKey> : IAggregateRoot
    {
        private int _version;
        private readonly List<DomainEvent> _events = [];

        /// <inheritdoc/>
        int IAggregateRoot.Version => _version;

        /// <inheritdoc/>
        IReadOnlyCollection<DomainEvent> IAggregateRoot.DomainEvents => _events.AsReadOnly();

        /// <summary>
        /// 主键
        /// </summary>
        public virtual TKey Id { get; protected set; } = default!;

        /// <inheritdoc/>
        void IAggregateRoot.ClearDomainEvents()
        {
            _events.Clear();
        }

        /// <inheritdoc/>
        void IAggregateRoot.RaiseEvent<T>(T domainEvent)
        {
            RaiseEvent(domainEvent);
        }

        /// <inheritdoc cref="IAggregateRoot.RaiseEvent{T}(T)"/>
        protected void RaiseEvent<T>(T domainEvent) where T : DomainEvent
        {
            _events.Add(domainEvent);

            if (this is IDomainEventApplier<T> applier)
            {
                applier.Apply(domainEvent);
                _version++;
            }
        }
    }
}
