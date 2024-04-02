namespace SharedKernel.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 回滚
        /// </summary>
        Task RollbackAsync(CancellationToken token = default);

        /// <summary>
        /// 保存
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
