using Audit;
using Audit.AuditProperties;
using Bamboo.Posts;
using Bamboo.Posts.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;

namespace Bamboo.EntityFrameworkCore
{
    /// <summary>
    /// Post 数据上下文
    /// </summary>
    public sealed class BlogDbContext(IServiceProvider serviceProvider, DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// 文章
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>(p =>
            {
                p.ToTable("Posts", "Blog");

                p.HasKey(p => p.Id).IsClustered(true);

                p.Property(p => p.Id).IsRequired().HasConversion(x => x.Value, x => new PostId(x));
                p.Property(p => p.Title).IsRequired().HasMaxLength(50);
                p.Property(p => p.Content).IsRequired().HasMaxLength(-1);
                p.Property(p => p.AuthorId).IsRequired();
                p.Property(p => p.PublicationTime).IsRequired();

                foreach (var property in AuditPropertiesManager.GetAuditProperties(typeof(Post)))
                {
                    property.Build(p);
                }
            });
        }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AuditHandleBeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AuditHandleBeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 保存之前审计处理
        /// </summary>
        private void AuditHandleBeforeSaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                var properties = AuditPropertiesManager.GetAuditProperties(entry.Metadata.ClrType);
                if (properties.Length == 0)
                {
                    continue;
                }

                var context = new AuditContext(serviceProvider, entry.State, entry);
                foreach (var property in properties)
                {
                    property.Write(context);
                }
            }
        }
    }
}
