using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 修改时间
    /// </summary>
    public class ModificationTimePropreryBuildDelegateCreator<TEntity> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<DateTime>(IHasModificationTime.Name).IsRequired();
        }
    }
}
