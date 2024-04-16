using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public class CreationTimePropreryBuildDelegateCreator<TEntity> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<DateTime>(IHasCreationTime.Name).IsRequired();
        }
    }
}
