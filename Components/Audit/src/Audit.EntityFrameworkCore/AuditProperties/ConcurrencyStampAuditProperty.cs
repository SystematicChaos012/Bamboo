using Microsoft.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    /// <summary>
    /// ConcurrencyStamp 审计属性
    /// </summary>
    internal sealed class ConcurrencyStampAuditProperty : AuditProperty
    {
        public override Property Create(Type entityType)
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
