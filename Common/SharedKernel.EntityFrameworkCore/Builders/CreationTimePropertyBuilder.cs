using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.Builders
{
    /// <summary>
    /// 创建审计属性
    /// </summary>
    public class CreationTimePropertyBuilder<TEntity> : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property<DateTime>(IHasCreationTime.Name).IsRequired();
        }

        private static CreationTimePropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new CreationTimePropertyBuilder<TEntity>();
        }
    }
}
