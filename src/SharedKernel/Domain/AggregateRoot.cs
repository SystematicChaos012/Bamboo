using System.Diagnostics;

namespace SharedKernel.Domain;

/// <summary>
/// 聚合根
/// </summary>
[DebuggerStepThrough]
public abstract class AggregateRoot
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
        Apply(domainEvent);
    }

    /// <summary>
    /// 应用领域事件
    /// </summary>
    public abstract void Apply(object domainEvent);
}