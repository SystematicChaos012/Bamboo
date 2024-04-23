using Audit.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 修改人审计属性
    /// </summary>
    internal sealed class ModifierAuditPropertyCreator : AuditPropertyCreator
    {
        /// <inheritdoc/>
        public override AuditProperty Create(Type entityType)
        {
            var type = AuditHelper.GetNullableTypeOfGenericArgument(entityType, typeof(IModifier<>), 0);
            var orignalType = type.GetGenericArguments()[0];

            return new (
                builder =>
                {
                    builder.Property(type, "Modifier");
                },
                context =>
                {
                    if (context.EntityState == EntityState.Modified)
                    {
                        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
                        context.EntityEntry.Property("Modifier").CurrentValue = AuditHelper.Parse(orignalType, currentUser.Id);
                    }
                }
            );
        }
    }
}
