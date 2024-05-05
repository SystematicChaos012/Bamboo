namespace SharedKernel.Domain;

/// <summary>
/// 实体
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// 触发事件
    /// </summary>
    public abstract void Raise<T>(T domainEvent) where T : DomainEvent;
}