using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// 属性构建委托创建器
    /// </summary>
    public abstract class PropertyBuildDelegateCreator<TEntity> where TEntity : class
    {
        /// <summary>
        /// 创建委托
        /// </summary>
        public abstract Action<EntityTypeBuilder<TEntity>> Create();
    }
}
