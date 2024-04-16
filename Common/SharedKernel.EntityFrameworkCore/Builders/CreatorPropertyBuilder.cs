using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bamboo.Builders
{
    /// <summary>
    /// 创建人审计
    /// </summary>
    public class CreatorPropertyBuilder<TEntity>(Type creatorType) : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; } = ReflectionHelper.GetGenericArgumentType(creatorType, 1)
                ?? throw new InvalidOperationException("invalid type");

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; } = ReflectionHelper.GetValueFromInterfaceStaticProperty<string>(creatorType, "Name");

        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property(PropertyType, PropertyName).IsRequired();
        }

        private static CreatorPropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new CreatorPropertyBuilder<TEntity>(ReflectionHelper.GetGenericArgumentType(interfaceType, 1)!);
        }
    }
}
