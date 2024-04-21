using Audit.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 修改人审计属性
    /// </summary>
    internal sealed class ModifierAuditProperty : AuditProperty
    {
        /// <inheritdoc/>
        public override (Action<EntityTypeBuilder> Builder, Action<AuditContext> Writer) Create(Type entityType)
        {
            var type = AuditHelper.GetNullableTypeOfGenericArgument(entityType, typeof(IModifier<>), 0);
            return (
                builder =>
                {
                    builder.Property(type, "Modifier");
                },
                context =>
                {
                    if (context.EntityState == EntityState.Modified)
                    {
                        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
                        context.EntityEntry.Property("Modifier").CurrentValue = AuditHelper.Parse(type, currentUser.Id);
                    }
                }
            );
        }
    }
}
