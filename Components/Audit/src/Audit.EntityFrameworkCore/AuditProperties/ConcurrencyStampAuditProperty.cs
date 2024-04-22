namespace Audit.AuditProperties
{
    /// <summary>
    /// ConcurrencyStamp 审计属性
    /// </summary>
    internal sealed class ConcurrencyStampAuditProperty : AuditProperty
    {
        public override Property Create(Type entityType)
        {
            return new (builder =>
            {
                builder.Property<Guid>("ConcurrencyStamp").IsRequired().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            }, _ => { });
        }
    }
}
