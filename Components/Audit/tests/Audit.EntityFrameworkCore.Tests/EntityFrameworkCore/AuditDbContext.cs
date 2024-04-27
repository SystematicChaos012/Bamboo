using Audit.AuditProperties;
using Microsoft.EntityFrameworkCore;

namespace Audit.EntityFrameworkCore
{
    public sealed class AuditDbContext(IServiceProvider serviceProvider, DbContextOptions<AuditDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyEntity>(builder => 
            {
                builder.ToTable("Entities");

                builder.HasKey(x => x.Id);

                builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                builder.Property(x => x.Name).HasMaxLength(50);

                foreach (var property in AuditPropertiesManager.GetAuditProperties(typeof(MyEntity)))
                {
                    property.Build(builder);
                }
            });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            HandleAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            HandleAuditProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void HandleAuditProperties()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                var auditPropertyContext = new AuditPropertyContext(serviceProvider, entry.State, entry);
                foreach (var property in AuditPropertiesManager.GetAuditProperties(entry.Metadata.ClrType))
                {
                    property.Write(auditPropertyContext);
                }
            }
        }
    }
}
