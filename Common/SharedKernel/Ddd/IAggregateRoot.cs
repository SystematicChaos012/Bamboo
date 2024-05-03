namespace SharedKernel.Ddd;

/// <summary>
/// 聚合根
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IAggregateRoot<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; }
}