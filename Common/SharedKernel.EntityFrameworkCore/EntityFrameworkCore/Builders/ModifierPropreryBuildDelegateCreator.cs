using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 修改人
    /// </summary>
    public class ModifierPropreryBuildDelegateCreator<TEntity, TKey> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<string>(IHasModifier<TKey>.Name).IsRequired().HasValueGenerator<ModifierValueGenerator>();
        }

        private class ModifierValueGenerator : ValueGenerator<TKey>
        {
            public override bool GeneratesTemporaryValues => false;

            public override TKey Next(EntityEntry entry)
            {
                return IHasModifier<TKey>.Default;
            }
        }
    }
}
