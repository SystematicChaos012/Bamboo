using Microsoft.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 逻辑删除审计属性
    /// </summary>
    internal sealed class LogicalDeletionAuditProperty : AuditProperty
    {
        public override Property Create(Type entityType)
        {
            return new (
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
