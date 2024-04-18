using Microsoft.EntityFrameworkCore;
using SharedKernel.Auditing;
using SharedKernel.Domain;

namespace Bamboo.EntityFrameworkCore
{
    /// <summary>
    /// 审计帮助类
    /// </summary>
    public static class AuditingHelper
    {
        // TODO: 更新状态通过审计
        public static void UpdateAuditingProperties(DbContext dbContext)
        {
            // 获取所有聚合根
            var entries = dbContext.ChangeTracker.Entries<IAggregateRoot>().ToArray();

            foreach (var entry in entries)
            {
                /*
                 * 暂未对 IHasCreator、IHasModifier、IHasDeleter 接口的实现进行审计
                 */

                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            if (entry.Entity is IHasCreationTime)
                            {
                                entry.Property(IHasCreationTime.Name).CurrentValue = DateTime.Now;
                            }

                            goto case EntityState.Modified;
                        }
                    case EntityState.Deleted:
                        {
                            if (entry.Entity is IHasLogicalDeletion)
                            {
                                entry.State = EntityState.Modified;
                                entry.Property(IHasLogicalDeletion.Name).CurrentValue = true;
                            }

                            if (entry.Entity is IHasDeletionTime)
                            {
                                entry.Property(IHasDeletionTime.Name).CurrentValue = DateTime.Now;
                            }

                            goto case EntityState.Modified;
                        }
                    case EntityState.Modified:
                        {
                            if (entry.Entity is IHasModificationTime)
                            {
                                entry.Property(IHasModificationTime.Name).CurrentValue = DateTime.Now;
                            }

                            break;
                        }
                }
            }
        }
    }
}
