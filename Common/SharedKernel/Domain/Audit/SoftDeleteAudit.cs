namespace SharedKernel.Domain.Audit
{
    /// <inheritdoc cref="ISoftDeleteAudit"/>
    public class SoftDeleteAudit : ISoftDeleteAudit
    {
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
