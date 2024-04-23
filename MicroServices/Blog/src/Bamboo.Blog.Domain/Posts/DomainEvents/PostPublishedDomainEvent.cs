using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts
{
    /// <summary>
    /// 文章发布领域事件
    /// </summary>
    /// <param name="Id">文章 Id</param>
    public record PostPublishedDomainEvent(PostId Id) : DomainEvent;
}