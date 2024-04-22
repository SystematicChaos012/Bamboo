using Audit.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 删除人审计属性
    /// </summary>
    internal sealed class DeleterAuditProperty : AuditProperty
    {
        /// <inheritdoc/>
        public override Property Create(Type entityType)
        {
            var type = AuditHelper.GetNullableTypeOfGenericArgument(entityType, typeof(IDeleter<>), 0);
            return new (
                builder =>
                {
                    builder.Property(type, "Deleter");
                },
                context =>
                {
                    if (context.EntityState == EntityState.Deleted)
                    {
                        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
                        context.EntityEntry.Property("Deleter").CurrentValue = AuditHelper.Parse(type, currentUser.Id);
                    }
                }
            );
        }
    }
}
