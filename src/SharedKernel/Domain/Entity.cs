using SharedKernel.Domain.Accessors;
using SharedKernel.Sequences;

namespace SharedKernel.Domain;

/// <summary>
/// 实体
/// </summary>
public abstract class Entity
{
#nullable disable

    /// <summary>
    /// 聚合序列号
    /// </summary>
    private SequentialValue _sequentialValue;

#nullable enable

    /// <summary>
    /// 领域事件
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// 应用领域事件
    /// </summary>
    public abstract void Apply(object domainEvent);

    /// <summary>
    /// 提交事件
    /// </summary>
    public virtual void Raise<T>(T domainEvent) where T : DomainEvent
    {
        _domainEvents.Add(domainEvent);
        Apply(domainEvent);

        if (_sequentialValue != null)
        {
            DomainEventUnsafeAccessor.Seq(domainEvent) = _sequentialValue.Next();
        }
    }
}