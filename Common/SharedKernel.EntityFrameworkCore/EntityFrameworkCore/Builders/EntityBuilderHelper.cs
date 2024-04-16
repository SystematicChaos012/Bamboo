using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bamboo.EntityFrameworkCore.Builders
{
    /// <summary>
    /// Helper
    /// </summary>
    internal class EntityBuilderHelper<TEntity> where TEntity : class
    {
        internal static Action<EntityTypeBuilder<TEntity>> GenerateCreationTimeBuilder()
        {
            return new CreationTimePropreryBuildDelegateCreator<TEntity>().Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateModificationTimeBuilder()
        {
            return new ModificationTimePropreryBuildDelegateCreator<TEntity>().Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateDeletionTimeBuilder()
        {
            return new DeletionTimePropreryBuildDelegateCreator<TEntity>().Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateLogicalDeletionBuilder()
        {
            return new LogicalDeletionPropreryBuildDelegateCreator<TEntity>().Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateVersionBuilder()
        {
            return new VersionPropreryBuildDelegateCreator<TEntity>().Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateCreatorBuilder(Type type)
        {
            var gType = typeof(CreatorPropreryBuildDelegateCreator<,>)
                .MakeGenericType(
                    typeof(TEntity),
                    ReflectionHelper.GetGenericArgumentType(type, 1)!);

            return (Activator.CreateInstance(gType) as PropertyBuildDelegateCreator<TEntity>)!.Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateModifierBuilder(Type type)
        {
            var gType = typeof(ModifierPropreryBuildDelegateCreator<,>)
                .MakeGenericType(
                    typeof(TEntity),
                    ReflectionHelper.GetGenericArgumentType(type, 1)!);

            return (Activator.CreateInstance(gType) as PropertyBuildDelegateCreator<TEntity>)!.Create();
        }

        internal static Action<EntityTypeBuilder<TEntity>> GenerateDeleterBuilder(Type type)
        {
            var gType = typeof(ModifierPropreryBuildDelegateCreator<,>)
                .MakeGenericType(
                    typeof(TEntity),
                    ReflectionHelper.GetGenericArgumentType(type, 1)!);

            return (Activator.CreateInstance(gType) as PropertyBuildDelegateCreator<TEntity>)!.Create();
        }
    }
}