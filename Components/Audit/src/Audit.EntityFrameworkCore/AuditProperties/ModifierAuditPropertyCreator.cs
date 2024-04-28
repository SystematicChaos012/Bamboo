using Audit.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Security;

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
                        var identityContext = context.ServiceProvider.GetRequiredService<IIdentityContext>();
                        if (identityContext.IsAuthenticated)
                        {
                            var options = context.ServiceProvider.GetRequiredService<IOptions<AuditOptions>>().Value;
                            context.EntityEntry.Property("Modifier").CurrentValue = AuditHelper.Parse(orignalType, identityContext.FindClaim(options.IdentityClaimType)!);
                        }
                    }
                }
            );
        }
    }
}
