using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts
{
    /// <summary>
    /// 文章标题已更改领域事件
    /// </summary>
    /// <param name="Id">Post Id</param>
    /// <param name="OldTitle">旧标题</param>
    /// <param name="NewTitle">新标题</param>
    public record class PostTitleChangedDomainEvent(PostId Id, string OldTitle, string NewTitle) : DomainEvent;
}