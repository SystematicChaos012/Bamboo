using Bamboo.Builders;
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
        internal class InternalCache<TEntity> where TEntity : class
        {
            internal static IPropertyBuilder<TEntity>[]? Builders { get; set; }
        }

        /// <summary>
        /// 具有审计属性
        /// </summary>
        public static void HasAuditingProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            foreach (var applier in GetOrCreateBuilders<TEntity>())
            {
                applier.Apply(builder);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        private static IPropertyBuilder<TEntity>[] GetOrCreateBuilders<TEntity>() where TEntity : class
        {
            if (InternalCache<TEntity>.Builders == null)
            {
                InternalCache<TEntity>.Builders = GetBuilders<TEntity>(typeof(TEntity)).ToArray();
            }

            return InternalCache<TEntity>.Builders;
        }

        /// <summary>
        /// 获取 Builder
        /// </summary>
        private static IEnumerable<IPropertyBuilder<TEntity>> GetBuilders<TEntity>(Type entityType) where TEntity : class
        {
            HashSet<Type> exists = [];

            foreach (var i in entityType.GetInterfaces())
            {
                // 获取定义
                var definition = i.IsGenericType ? i.GetGenericTypeDefinition() : i;

                if (exists.Contains(definition))
                    continue;

                if (definition == typeof(IHasVersion))
                    yield return VersionPropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasCreationTime))
                    yield return CreationTimePropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasCreator<>))
                    yield return CreatorPropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasModificationTime))
                    yield return ModificationTimePropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasModifier<>))
                    yield return ModifierPropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasDeletionTime))
                    yield return DeletionTimePropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasDeleter<>))
                    yield return DeleterPropertyBuilder<TEntity>.GetOrCreate(i);

                if (definition == typeof(IHasLogicalDeletion))
                    yield return LogicalDeletionPropertyBuilder<TEntity>.GetOrCreate(i);

                exists.Add(definition);
            }
        }
    }
}
