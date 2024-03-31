namespace SharedKernel.Domain
{
    /// <summary>
    /// 事件应用
    /// </summary>
    public interface IDomainEventApplier<T> where T : DomainEvent
    {
        /// <summary>
        /// 应用事件
        /// </summary>
        /// <param name="domainEvent">领域事件</param>
        void Apply(T domainEvent);
    }
}
