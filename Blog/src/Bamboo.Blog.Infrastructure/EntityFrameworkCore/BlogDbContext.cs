using Bamboo.Posts;
using Microsoft.EntityFrameworkCore;
using SharedKernel.EntityFrameworkCore;

namespace Bamboo.EntityFrameworkCore
{
    /// <summary>
    /// Post 数据上下文
    /// </summary>
    public sealed class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Post>(p =>
            {
                p.ToTable("Posts", "Blog");

                p.HasKey(p => p.Id).IsClustered(true);

                p.Property(p => p.Id).UseIdentityColumn(1, 1).IsRequired();
                p.Property(p => p.Title).IsRequired().HasMaxLength(50);
                p.Property(p => p.Content).IsRequired().HasMaxLength(-1);
                p.Property(p => p.AuthorId).IsRequired();
                p.Property(p => p.PublicationTime).IsRequired();

                p.HasAuditProperties();
            });
        }
    }
}
