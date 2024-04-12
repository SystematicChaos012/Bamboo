using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bamboo.Builders
{
    /// <summary>
    /// Builder
    /// </summary>
    public interface IPropertyBuilder<TEntity> where TEntity : class
    {
        /// <summary>
        /// 应用
        /// </summary>
        PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder);
    }
}
