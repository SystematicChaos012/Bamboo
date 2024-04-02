namespace SharedKernel.Domain.Audit
{
    /// <inheritdoc cref="ICreationAudit"/>
    public sealed class CreationAudit : ICreationAudit
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
