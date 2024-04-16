using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.Builders
{
    /// <summary>
    /// 逻辑删除审计属性
    /// </summary>
    public class LogicalDeletionPropertyBuilder<TEntity> : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property<bool>(IHasLogicalDeletion.Name).IsRequired();
        }

        private static LogicalDeletionPropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new LogicalDeletionPropertyBuilder<TEntity>();
        }
    }
}
