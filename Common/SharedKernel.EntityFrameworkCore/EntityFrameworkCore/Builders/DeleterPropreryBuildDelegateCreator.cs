using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 删除人
    /// </summary>
    public class DeleterPropreryBuildDelegateCreator<TEntity, TKey> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<string>(IHasDeleter<TKey>.Name).IsRequired().HasValueGenerator<DeleterValueGenerator>();
        }

        private class DeleterValueGenerator : ValueGenerator<TKey>
        {
            public override bool GeneratesTemporaryValues => false;

            public override TKey Next(EntityEntry entry)
            {
                return IHasDeleter<TKey>.Default;
            }
        }
    }
}
