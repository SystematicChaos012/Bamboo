
namespace Bamboo.Posts
{
    /// <inheritdoc/>
    public sealed class PostRepository : IPostRepository
    {
        public ValueTask<Post?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
