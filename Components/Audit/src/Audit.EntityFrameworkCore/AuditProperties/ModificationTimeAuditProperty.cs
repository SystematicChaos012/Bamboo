using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 修改时间审计属性
    /// </summary>
    internal sealed class ModificationTimeAuditProperty : AuditProperty
    {
        /// <summary>
        /// 启用本地时间
        /// </summary>
        public static bool EnableLocalTime { get; set; } = false;

        /// <inheritdoc/>
        public override (Action<EntityTypeBuilder> Builder, Action<AuditContext> Writer) Create(Type entityType)
        {
            return (
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
