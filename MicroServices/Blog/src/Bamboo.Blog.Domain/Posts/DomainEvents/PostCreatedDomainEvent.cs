using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents
{
    /// <summary>
    /// Post 创建领域事件
    /// </summary>
    public sealed record PostCreatedDomainEvent(Guid Id, string Title, string Content, Guid AuthorId, DateTime PublicationTime) : DomainEvent;
}
