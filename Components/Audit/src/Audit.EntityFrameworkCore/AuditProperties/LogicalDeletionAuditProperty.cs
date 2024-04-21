using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 逻辑删除审计属性
    /// </summary>
    internal sealed class LogicalDeletionAuditProperty : AuditProperty
    {
        public override (Action<EntityTypeBuilder> Builder, Action<AuditContext> Writer) Create(Type entityType)
        {
            return (
                builder =>
                {
                    builder.Property<bool>("IsDeleted").IsRequired();
                },
                context =>
                {
                    if (context.EntityState == EntityState.Deleted)
                    {
                        context.EntityEntry.Property("IsDeleted").CurrentValue = true;
                        context.EntityEntry.State = EntityState.Modified;
                    }
                }
            );
        }
    }
}
