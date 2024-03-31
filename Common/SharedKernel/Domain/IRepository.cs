namespace SharedKernel.Domain
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IRepository<TKey, T>
    {
        /// <summary>
        /// 通过 Id 查找
        /// </summary>
        ValueTask<T?> FindAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
