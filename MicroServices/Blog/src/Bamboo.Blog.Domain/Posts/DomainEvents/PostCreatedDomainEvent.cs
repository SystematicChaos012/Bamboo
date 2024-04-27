using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents
{
    /// <summary>
    /// 创建领域事件
    /// </summary>
    /// <param name="Id">Id</param>
    /// <param name="Title">标题</param>
    /// <param name="Content">内容</param>
    public sealed record class PostCreatedDomainEvent(PostId Id, string Title, string Content) : DomainEvent;
}
