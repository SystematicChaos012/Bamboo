using SharedKernel.Domain.Accessors;
using SharedKernel.Domain.Collections;
using SharedKernel.Sequences;
using System.Diagnostics;

namespace SharedKernel.Domain;

/// <summary>
/// 聚合根
/// </summary>
public abstract class AggregateRoot
{
    /// <summary>
    /// 聚合序列号
    /// </summary>
    private readonly SequentialValue _sequentialValue = new();

    /// <summary>
    /// 领域事件
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// 应用领域事件
    /// </summary>
    public abstract void Apply(object domainEvent);

    /// <summary>
    /// 追踪实体
    /// </summary>
    public virtual T Track<T>(T entity) where T : Entity
    {
        EntityUnsafeAccessor.SequentialValue(entity) = _sequentialValue;
        return entity;
    }

    /// <summary>
    /// 追踪实体
    /// </summary>
    public virtual ICollection<T> CreateTrackCollection<T>() where T : Entity => new TrackCollection<T>(_sequentialValue);

    /// <summary>
    /// 提交事件
    /// </summary>
    public virtual void Raise<T>(T domainEvent) where T : DomainEvent
    {
        _domainEvents.Add(domainEvent);
        Apply(domainEvent);

        DomainEventUnsafeAccessor.Seq(domainEvent) = _sequentialValue.Next();
    }
}
