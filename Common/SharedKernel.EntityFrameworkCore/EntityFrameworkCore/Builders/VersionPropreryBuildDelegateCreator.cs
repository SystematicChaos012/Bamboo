using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 版本
    /// </summary>
    public class VersionPropreryBuildDelegateCreator<TEntity> : PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <inheritdoc/>
        public override Action<EntityTypeBuilder<TEntity>> Create()
        {
            return (builder) => builder.Property<int>(IHasVersion.Name).IsRequired();
        }
    }
}
