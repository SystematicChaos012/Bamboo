using Audit.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 创建人审计属性
    /// </summary>
    internal sealed class CreatorAuditProperty : AuditProperty
    {
        /// <inheritdoc/>
        public override Property Create(Type entityType)
        {
            var type = AuditHelper.GetNullableTypeOfGenericArgument(entityType, typeof(ICreator<>), 0);
            return new (
                builder =>
                {
                    builder.Property(type, "Creator");
                },
                context =>
                {
                    if (context.EntityState == EntityState.Added)
                    {
                        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
                        context.EntityEntry.Property("Creator").CurrentValue = AuditHelper.Parse(type, currentUser.Id);
                    }
                }
            );
        }
    }
}
