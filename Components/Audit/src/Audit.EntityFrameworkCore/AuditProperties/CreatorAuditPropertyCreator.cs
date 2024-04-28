using Audit.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Security;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 创建人审计属性
    /// </summary>
    internal sealed class CreatorAuditPropertyCreator : AuditPropertyCreator
    {
        /// <inheritdoc/>
        public override AuditProperty Create(Type entityType)
        {
            var type = AuditHelper.GetNullableTypeOfGenericArgument(entityType, typeof(ICreator<>), 0);
            var orignalType = type.GetGenericArguments()[0];

            return new (
                builder =>
                {
                    builder.Property(type, "Creator");
                },
                context =>
                {
                    if (context.EntityState == EntityState.Added)
                    {
                        var identityContext = context.ServiceProvider.GetRequiredService<IIdentityContext>();
                        if (identityContext.IsAuthenticated)
                        {
                            var options = context.ServiceProvider.GetRequiredService<IOptions<AuditOptions>>().Value;
                            context.EntityEntry.Property("Creator").CurrentValue = AuditHelper.Parse(orignalType, identityContext.FindClaim(options.IdentityClaimType)!);
                        }
                    }
                }
            );
        }
    }
}
