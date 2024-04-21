using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 审计属性
    /// </summary>
    internal abstract class AuditProperty
    {
        /// <summary>
        /// 创建审计属性的构建和写操作
        /// </summary>
        public abstract (Action<EntityTypeBuilder> Builder, Action<AuditContext> Writer) Create(Type entityType);
    }
}
