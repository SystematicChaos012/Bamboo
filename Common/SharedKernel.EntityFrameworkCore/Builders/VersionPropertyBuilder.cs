using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.Builders
{
    /// <summary>
    /// 版本审计
    /// </summary>
    public class VersionPropertyBuilder<TEntity> : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property<int>(IHasVersion.Name).IsRequired();
        }

        private static VersionPropertyBuilder<TEntity>? _instance;

        /// <inheritdoc/>
        public static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new VersionPropertyBuilder<TEntity>();
        }
    }
}
