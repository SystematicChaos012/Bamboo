
using Bamboo.EntityFrameworkCore;

namespace Bamboo.Posts
{
    /// <inheritdoc/>
    public sealed class PostRepository(BlogDbContext dbContext) : IPostRepository
    {
        /// <inheritdoc/>
        public ValueTask<Post?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return dbContext.Posts.FindAsync([id], cancellationToken: cancellationToken);
        }
    }
}
