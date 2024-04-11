using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SharedKernel.EntityFrameworkCore
{
    /// <summary>
    /// EntityEntry 扩展
    /// </summary>
    public static class EntityEntryExtensions
    {
        /// <summary>
        /// 更新审计属性
        /// </summary>
        public static void UpdateAuditProperties<TEntity>(this IEnumerable<EntityEntry<TEntity>> entries) where TEntity : class
        {
            // foreach (var entry in entries)
            // {
            //     if (entry is { State: EntityState.Added, Entity: IHasCreationTime })
            //     {
            //         entry.Property<DateTime>(AuditConsts.CREATION_TIME_NAME).CurrentValue = DateTime.UtcNow;
            //         continue;
            //     }
            // 
            //     if (entry is { State: EntityState.Modified, Entity: IHasModificationTime })
            //     {
            //         entry.Property<DateTime>(AuditConsts.MODIFICATION_TIME_NAME).CurrentValue = DateTime.UtcNow;
            //         continue;
            //     }
            // 
            //     if (entry is { State: EntityState.Deleted, Entity: IHasDeletedFlag })
            //     {
            //         entry.State = EntityState.Modified;
            //         entry.Property<bool>(AuditConsts.DELETED_FLAG_NAME).CurrentValue = true;
            //         continue;
            //     }
            // }
        }
    }
}
