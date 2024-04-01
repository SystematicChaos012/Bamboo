using Bamboo.EntityFrameworkCore;
using Bamboo.Posts.DomainEvents.V1;
using MediatR;

namespace Bamboo.Posts.DomainEventHandlers
{
    /// <summary>
    /// Post 删除领域事件
    /// </summary>
    public sealed class PostDeletedDomainEventHandler(BlogDbContext dbContext) : INotificationHandler<PostDeletedDomainEvent>
    {
        public async Task Handle(PostDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await dbContext.Set<Post>().FindAsync([notification.Id], cancellationToken);
            if (post == null)
            {
                return;
            }

            dbContext.Remove(post);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
