using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.Builders
{
    /// <summary>
    /// 修改审计属性
    /// </summary>
    public class ModificationTimePropertyBuilder<TEntity> : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property<DateTime>(IHasModificationTime.Name).IsRequired();
        }

        private static ModificationTimePropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new ModificationTimePropertyBuilder<TEntity>();
        }
    }
}
