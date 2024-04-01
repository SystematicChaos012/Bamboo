using SharedKernel.Domain;

namespace Bamboo.Posts
{
    /// <summary>
    /// Post 仓储
    /// </summary>
    public interface IPostRepository : IRepository<Guid, Post>
    {
    }
}
