using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents
{
    /// <summary>
    /// 文章内容变更领域事件
    /// </summary>
    /// <param name="Id">Post Id</param>
    /// <param name="OldContent">旧内容</param>
    /// <param name="NewContent">新内容</param>
    public sealed record class PostContentChangedDomainEvent(PostId Id, string OldContent, string NewContent) : DomainEvent;
}