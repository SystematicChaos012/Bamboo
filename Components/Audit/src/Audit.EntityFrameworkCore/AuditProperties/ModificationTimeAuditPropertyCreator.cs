using Microsoft.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 修改时间审计属性
    /// </summary>
    internal sealed class ModificationTimeAuditPropertyCreator : AuditPropertyCreator
    {
        /// <summary>
        /// 启用本地时间
        /// </summary>
        public static bool EnableLocalTime { get; set; } = false;

        /// <inheritdoc/>
        public override AuditProperty Create(Type entityType)
        {
            return new (
                builder =>
                {
                    builder.Property<DateTime?>("ModificationTime");
                },
                context =>
                {
                    if (context.EntityState == EntityState.Modified)
                    {
                        context.EntityEntry.Property("ModificationTime").CurrentValue = EnableLocalTime ? DateTime.Now : DateTime.UtcNow;
                    }
                }
            );
        }
    }
}
