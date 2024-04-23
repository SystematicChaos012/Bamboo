using Microsoft.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    /// <summary>
    /// ConcurrencyStamp 审计属性
    /// </summary>
    internal sealed class ConcurrencyStampAuditPropertyCreator : AuditPropertyCreator
    {
        public override AuditProperty Create(Type entityType)
        {
            return new (
                builder =>
                {
                    builder.Property<Guid>("ConcurrencyStamp").IsConcurrencyToken();
                }, 
                context => 
                {
                    if (context.EntityState is EntityState.Added or EntityState.Modified)
                    {
                        context.EntityEntry.Property("ConcurrencyStamp").CurrentValue = Guid.NewGuid();
                    }
                });
        }
    }
}
