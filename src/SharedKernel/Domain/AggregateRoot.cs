namespace SharedKernel.Domain;

/// <summary>
/// 聚合根
/// </summary>
public abstract class AggregateRoot
{
    /// <summary>
    /// 版本
    /// </summary>
    private int _version;
    
    /// <summary>
    /// 领域事件
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// 提交事件
    /// </summary>
    public virtual void Raise<T>(T domainEvent) where T : DomainEvent
    {
        _version++;
        _domainEvents.Add(domainEvent);
        if (this is IDomainEventApplier<T> applier)
        {
            applier.Apply(domainEvent);
        }
    }
}