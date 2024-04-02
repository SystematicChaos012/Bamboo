using SharedKernel.Domain;
using SharedKernel.UnitOfWork;
using SharedKernel.EntityFrameworkCore;

namespace Bamboo.EntityFrameworkCore
{
    /// <inheritdoc/>
    public sealed class UnitOfWork(BlogDbContext dbContext) : IUnitOfWork, IDisposable
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

            dbContext.ChangeTracker.Entries<IAggregateRoot>().UpdateAuditProperties();
        }

        public void Dispose()
        {
            dbContext.Database.CurrentTransaction?.Dispose();
        }
    }
}
