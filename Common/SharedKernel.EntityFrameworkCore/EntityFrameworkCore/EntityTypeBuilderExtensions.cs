using Bamboo.EntityFrameworkCore.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Auditing;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// 内部缓存
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        internal class InternalCache<TEntity> where TEntity : class
        {
            public static bool IsInitialized { get; internal set; } = false;

            public static Action<EntityTypeBuilder<TEntity>>[] Builders { get; internal set; } = [];
        }

        /// <summary>
        /// 具有审计属性
        /// </summary>
        public static void HasAuditingProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            if (InternalCache<TEntity>.IsInitialized is false)
            {
                Initialize<TEntity>();
            }

            foreach (var action in InternalCache<TEntity>.Builders)
            {
                action(builder);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize<TEntity>() where TEntity : class
        {
            InternalCache<TEntity>.IsInitialized = true;

            var set = new HashSet<Type>();
            var list = new List<Action<EntityTypeBuilder<TEntity>>>();

            foreach (var (type, definition) in GetInterfacesRecursive(typeof(TEntity)))
            {
                if (set.Contains(definition))
                {
                    continue;
                }

                if (typeof(IHasCreationTime).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateCreationTimeBuilder());

                if (typeof(IHasModificationTime).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateModificationTimeBuilder());

                if (typeof(IHasDeletionTime).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateDeletionTimeBuilder());

                if (typeof(IHasDeleter<>).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateDeleterBuilder(type));

                if (typeof(IHasModifier<>).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateModifierBuilder(type));

                if (typeof(IHasCreator<>).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateCreatorBuilder(type));

                if (typeof(IHasVersion).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateVersionBuilder());

                if (typeof(IHasLogicalDeletion).Equals(definition))
                    list.Add(EntityBuilderHelper<TEntity>.GenerateLogicalDeletionBuilder());

                set.Add(definition);
            }

            InternalCache<TEntity>.Builders = list.ToArray();
        }

        /// <summary>
        /// 递归获取接口
        /// </summary>
        private static IEnumerable<(Type Type, Type Definition)> GetInterfacesRecursive(Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                yield return i.IsGenericType 
                    ? (i, i.GetGenericTypeDefinition())
                    : (i, i);

                foreach (var subInterface in GetInterfacesRecursive(i))
                {
                    yield return subInterface;
                }
            }
        }
    }
}
