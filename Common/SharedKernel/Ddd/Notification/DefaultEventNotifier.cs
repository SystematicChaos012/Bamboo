using System.Reflection;

namespace SharedKernel.Ddd.Notification;

/// <summary>
/// 默认事件通知
/// </summary>
public class DefaultEventNotifier<T> : EventNotifier
{
    /// <summary>
    /// 聚合根
    /// </summary>
    private T AggregateRoot { get; }

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
        /* NET 9 Preview 4 发布将替换为 UnsafeAccessor 版本 */
        var methodInfo = AggregateRoot!.GetType().GetMethod("Raise", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var genericMethodInfo = methodInfo.MakeGenericMethod(typeof(TEvent));
        genericMethodInfo.Invoke(AggregateRoot, [domainEvent]);
    }
}