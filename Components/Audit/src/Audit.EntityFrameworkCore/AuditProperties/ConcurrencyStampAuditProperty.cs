using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.AuditProperties
{
    /// <summary>
    /// ConcurrencyStamp 审计属性
    /// </summary>
    internal sealed class ConcurrencyStampAuditProperty : AuditProperty
    {
        public override (Action<EntityTypeBuilder> Builder, Action<AuditContext> Writer) Create(Type entityType)
        {
            return (builder =>
            {
                builder.Property<Guid>("ConcurrencyStamp").IsRequired().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
            }, _ => { });
        }
    }
}
