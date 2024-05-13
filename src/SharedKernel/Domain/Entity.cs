namespace SharedKernel.Domain;

/// <summary>
/// 实体
/// </summary>
public class Entity
{
    /// <summary>
    /// 领域事件
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// 提交事件
    /// </summary>
    public virtual void Raise<T>(T domainEvent) where T : DomainEvent
    {
        _domainEvents.Add(domainEvent);
        if (this is IDomainEventApplier<T> applier)
        {
            applier.Apply(domainEvent);
        }
    }
}