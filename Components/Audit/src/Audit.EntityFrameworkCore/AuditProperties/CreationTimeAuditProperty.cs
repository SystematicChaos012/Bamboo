using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 创建时间审计属性
    /// </summary>
    internal sealed class CreationTimeAuditProperty : AuditProperty
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
                    builder.Property<DateTime>("CreationTime").IsRequired();
                },
                context =>
                {
                    if (context.EntityState == EntityState.Added)
                    {
                        context.EntityEntry.Property("CreationTime").CurrentValue = EnableLocalTime ? DateTime.Now : DateTime.UtcNow;
                    }
                }
            );
        }
    }
}
