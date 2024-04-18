using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 创建人
    /// </summary>
    public class CreatorPropreryBuildDelegateCreator<TEntity, TKey> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<TKey>(IHasCreator<TKey>.Name).IsRequired().HasValueGenerator<CreatorValueGenerator>();
        }

        private class CreatorValueGenerator : ValueGenerator<TKey>
        {
            public override bool GeneratesTemporaryValues => false;

            public override TKey Next(EntityEntry entry)
            {
                return IHasCreator<TKey>.Default;
            }
        }
    }
}
