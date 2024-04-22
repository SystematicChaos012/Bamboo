using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents
{
    /// <summary>
    /// Post 删除领域事件
    /// </summary>
    public sealed record class PostDeletedDomainEvent(PostId Id) : DomainEvent;
}
