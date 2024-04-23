using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents
{
    /// <summary>
    /// 主题标题已更改领域事件
    /// </summary>
    public record PostTitleChangedDomainEvent(PostId Id, string Content) : DomainEvent
    {
    }
}
