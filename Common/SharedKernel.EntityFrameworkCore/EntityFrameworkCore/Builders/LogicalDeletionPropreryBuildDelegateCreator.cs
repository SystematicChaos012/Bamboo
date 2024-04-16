using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public class LogicalDeletionPropreryBuildDelegateCreator<TEntity> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<bool>(IHasLogicalDeletion.Name).IsRequired();
        }
    }
}
