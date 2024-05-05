namespace SharedKernel.Domain;

/// <summary>
/// 领域事件应用
/// </summary>
/// <typeparam name="T">领域事件类型</typeparam>
public interface IDomainEventApplier<T> where T : DomainEvent
{
    /// <summary>
    /// 应用事件
    /// </summary>
    /// <param name="domainEvent">领域事件</param>
    void Apply(T domainEvent);
}