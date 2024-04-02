using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedKernel.Domain.Audit;
using System.Linq.Expressions;

namespace SharedKernel.EntityFrameworkCore
{
    /// <summary>
    /// Source from abp
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// This method is used to add a query filter to this entity which combine with ABP EF Core builtin query filters.
        /// </summary>
        /// <returns></returns>
        public static EntityTypeBuilder<TEntity> HasQueryFilter<TEntity>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, bool>> filter)
            where TEntity : class
        {
#pragma warning disable EF1001
            var queryFilterAnnotation = builder.Metadata.FindAnnotation(CoreAnnotationNames.QueryFilter);
#pragma warning restore EF1001
            if (queryFilterAnnotation != null && queryFilterAnnotation.Value != null && queryFilterAnnotation.Value is Expression<Func<TEntity, bool>> existingFilter)
            {
                filter = QueryFilterExpressionHelper.CombineExpressions(filter, existingFilter);
            }

            return builder.HasQueryFilter(filter);
        }

        public static EntityTypeBuilder<TEntity> HasAuditProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDeleteAudit)))
            {
                Expression<Func<TEntity, bool>> expression = p => !EF.Property<bool>(p, nameof(SoftDeleteAudit.IsDeleted));
                builder.Property<bool>(nameof(SoftDeleteAudit.IsDeleted)).IsRequired();
                builder.HasQueryFilter(expression);
            }

            if (typeof(TEntity).IsAssignableFrom(typeof(ICreationAudit)))
            {
                builder.Property<DateTime>(nameof(CreationAudit.CreationTime)).IsRequired();
            }

            if (typeof(TEntity).IsAssignableFrom(typeof(IModificationAudit)))
            {
                builder.Property<DateTime>(nameof(ModificationAudit.ModificationTime)).IsRequired();
            }

            return builder;
        }
    }
}
