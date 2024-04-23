namespace Audit.AuditProperties
{
    /// <summary>
    /// ConcurrencyStamp 审计属性
    /// </summary>
    internal sealed class ConcurrencyStampAuditPropertyCreator : AuditPropertyCreator
    {
        public override AuditProperty Create(Type entityType)
        {
            return new(
                builder =>
                {
                    builder.Property<Guid>("ConcurrencyStamp").IsConcurrencyToken();
                },
                context =>
                {
                    context.EntityEntry.Property("ConcurrencyStamp").CurrentValue = Guid.NewGuid();
                });
        }
    }
}
