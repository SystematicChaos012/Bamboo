using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents
{
    /// <summary>
    /// 文章作者添加领域事件
    /// </summary>
    /// <param name="Id">作者 Id</param>
    /// <param name="PostId">文章 Id</param>
    /// <param name="Name">作者名称</param>
    public record class PostAuthorAddedDomainEvent(PostAuthorId Id, PostId PostId, string Name) : DomainEvent;
}
