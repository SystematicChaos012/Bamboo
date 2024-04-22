using Microsoft.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 删除时间审计属性
    /// </summary>
    internal sealed class DeletionTimeAuditProperty : AuditProperty
    {
        /// <summary>
        /// 启用本地时间
        /// </summary>
        public static bool EnableLocalTime { get; set; } = false;

        /// <inheritdoc/>
        public override Property Create(Type entityType)
        {
            return new (
                builder =>
                {
                    builder.Property<DateTime?>("DeletionTime").IsRequired();
                },
                context =>
                {
                    if (context.EntityState == EntityState.Deleted)
                    {
                        context.EntityEntry.Property("DeletionTime").CurrentValue = EnableLocalTime ? DateTime.Now : DateTime.UtcNow;
                    }
                }
            );
        }
    }
}
