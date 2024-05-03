namespace SharedKernel.Ddd.Events;

/// <summary>
/// 默认事件通知
/// </summary>
public class DefaultEventNotifier<T> : EventNotifier
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public T AggregateRoot { get; set; }

    /// <summary>
    /// 创建事件通知
    /// </summary>
    public DefaultEventNotifier(T aggregateRoot)
    {
        AggregateRoot = aggregateRoot;
    }
    
    /// <inheritdoc />
    public override void Raise<TEvent>(TEvent domainEvent)
    {
        
    }
}