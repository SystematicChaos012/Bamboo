namespace SharedKernel.Domain.Audit
{
    /// <inheritdoc cref="IModificationAudit"/>
    public class ModificationAudit : IModificationAudit
    {
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModificationTime { get; set; } = DateTime.UtcNow;
    }
}
