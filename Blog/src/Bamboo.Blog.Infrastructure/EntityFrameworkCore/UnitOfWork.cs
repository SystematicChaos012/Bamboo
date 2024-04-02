using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;
using SharedKernel.Domain.Audit;
using SharedKernel.UnitOfWork;

namespace Bamboo.EntityFrameworkCore
{
    /// <inheritdoc/>
    public sealed class UnitOfWork(BlogDbContext dbContext) : IUnitOfWork
    {
        /// <inheritdoc/>
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (dbContext.Database.CurrentTransaction is not null)
            {
                return dbContext.Database.CommitTransactionAsync(cancellationToken);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (dbContext.Database.CurrentTransaction is not null)
            {
                return dbContext.Database.RollbackTransactionAsync(cancellationToken);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (dbContext.Database.CurrentTransaction is null)
            {
                await dbContext.Database.BeginTransactionAsync(cancellationToken);
            }

            // 审计
            foreach (var entry in dbContext.ChangeTracker.Entries<IAggregateRoot>())
            {
                if (entry is { State: EntityState.Added, Entity: ICreationAudit })
                {
                    entry.Property<DateTime>(nameof(CreationAudit.CreationTime)).CurrentValue = DateTime.UtcNow;
                    continue;
                }

                if (entry is { State: EntityState.Modified, Entity: IModificationAudit })
                {
                    entry.Property<DateTime>(nameof(ModificationAudit.ModificationTime)).CurrentValue = DateTime.UtcNow;
                    continue;
                }

                if (entry is { State: EntityState.Deleted, Entity: ISoftDeleteAudit })
                {
                    entry.State = EntityState.Modified;
                    entry.Property<bool>(nameof(SoftDeleteAudit.IsDeleted)).CurrentValue = true;
                    continue;
                }
            }

            
        }
    }
}
