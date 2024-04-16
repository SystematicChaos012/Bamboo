namespace Bamboo.Builders
{
    /// <summary>
    /// Builder
    /// </summary>
    public interface IPropertyBuilderWithCache<TEntity> : IPropertyBuilder<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获取或创建
        /// </summary>
        abstract static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType);
    }
}
