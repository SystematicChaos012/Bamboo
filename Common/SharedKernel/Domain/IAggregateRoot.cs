namespace SharedKernel.Domain
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// 版本
        /// </summary>
        int Version { get; }

        /// <summary>
        /// 领域事件
        /// </summary>
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }

        /// <summary>
        /// 清理所有领域事件
        /// </summary>
        void ClearDomainEvents();

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="domainEvent">事件实例</param>
        void RaiseEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
