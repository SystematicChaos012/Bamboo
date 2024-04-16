using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bamboo.Builders
{
    /// <summary>
    /// 修改人审计
    /// </summary>
    public class ModifierPropertyBuilder<TEntity>(Type modifierType) : IPropertyBuilderWithCache<TEntity> where TEntity : class
    {
        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType { get; } = ReflectionHelper.GetGenericArgumentType(modifierType, 1)
                ?? throw new InvalidOperationException("invalid type");

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; } = ReflectionHelper.GetValueFromInterfaceStaticProperty<string>(modifierType, "Name");

        /// <inheritdoc/>
        public PropertyBuilder Apply(EntityTypeBuilder<TEntity> builder)
        {
            return builder.Property(PropertyType, PropertyName).IsRequired();
        }

        private static ModifierPropertyBuilder<TEntity>? _instance;
        public static IPropertyBuilder<TEntity> GetOrCreate(Type interfaceType)
        {
            return _instance ??= new ModifierPropertyBuilder<TEntity>(ReflectionHelper.GetGenericArgumentType(interfaceType, 1)!);
        }
    }
}
