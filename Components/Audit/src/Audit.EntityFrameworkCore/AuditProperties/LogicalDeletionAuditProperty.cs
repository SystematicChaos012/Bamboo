using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Audit.AuditProperties
{
    /// <summary>
    /// 逻辑删除审计属性
    /// </summary>
    internal sealed class LogicalDeletionAuditProperty : AuditProperty
    {
        public override Property Create(Type entityType)
        {
            return new (
                builder =>
                {
                    builder.Property<bool>("IsDeleted").IsRequired();

                    var parameter = Expression.Parameter(entityType, "b");
                    var property = Expression.Call(typeof(EF), nameof(EF.Property), [typeof(bool)], parameter, Expression.Constant("IsDeleted"));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    builder.HasQueryFilter(Expression.Lambda(condition, parameter));
                },
                context =>
                {
                    if (context.EntityState == EntityState.Deleted)
                    {
                        context.EntityEntry.Property("IsDeleted").CurrentValue = true;
                        context.EntityEntry.State = EntityState.Modified;
                    }
                }
            );
        }
    }
}
