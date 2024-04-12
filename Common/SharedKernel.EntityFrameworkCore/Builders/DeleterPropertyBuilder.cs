using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bamboo.Builders
{
    /// <summary>
    /// 删除人审计
    /// </summary>
    public class DeleterPropertyBuilder<TEntity>(Type deleterType) : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; } = ReflectionHelper.GetGenericArgumentType(deleterType, 1)
                ?? throw new InvalidOperationException("invalid type");

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; } = ReflectionHelper.GetValueFromInterfaceStaticProperty<string>(deleterType, "Name");

        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property(PropertyType, PropertyName).IsRequired();
        }

        private static DeleterPropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilderWithCache<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new DeleterPropertyBuilder<TEntity>(ReflectionHelper.GetGenericArgumentType(interfaceType, 1)!);
        }
    }
}
