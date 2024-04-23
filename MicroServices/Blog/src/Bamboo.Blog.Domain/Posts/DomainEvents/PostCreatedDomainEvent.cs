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
    /// <param name="PostedTime">发布时间</param>
    public record class PostCreatedDomainEvent(PostId Id, string Title, string Content, DateTime PostedTime) : DomainEvent;
}
