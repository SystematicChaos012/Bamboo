namespace SharedKernel.Ddd.Events;

/// <summary>
/// Applier
/// </summary>
public interface IApplier<T> where T : DomainEvent
{
    /// <summary>
    /// 应用领域事件
    /// </summary>
    void Apply(T domainEvent);
}