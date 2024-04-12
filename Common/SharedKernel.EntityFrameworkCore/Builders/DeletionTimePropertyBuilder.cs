using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.Builders
{
    /// <summary>
    /// 删除审计属性
    /// </summary>
    public class DeletionTimePropertyBuilder<TEntity> : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property<DateTime>(IHasDeletionTime.Name).IsRequired();
        }

        private static DeletionTimePropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilderWithCache<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new DeletionTimePropertyBuilder<TEntity>();
        }
    }
}
