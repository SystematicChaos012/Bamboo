using Blog.Core.Topics.DomainEvents;
using Blog.Core.Topics.Entities;
using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics;

/// <summary>
/// 主题
/// </summary>
public sealed class Topic : AggregateRoot
{
    /// <summary>
    /// 主键
    /// </summary>
    public TopicId Id { get; private set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; private set; } = null!;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; private set; } = null!;

    /// <summary>
    /// 作者
    /// </summary>
    public ICollection<TopicAuthor> Authors { get; } = [];

    /// <summary>
    /// 创建主题
    /// </summary>
    /// <param name="id">主键 Id</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    public Topic(TopicId id, string title, string content) =>
        Raise(new TopicCreatedDomainEvent(id, title, content));

    /// <summary>
    /// 修改标题
    /// </summary>
    /// <param name="title">新标题</param>
    public void ChangeTitle(string title) =>
        Raise(new TopicTitleChangedDomainEvent(Id, Title, title));

    /// <summary>
    /// 修改内容
    /// </summary>
    /// <param name="content">新内容</param>
    public void ChangeContent(string content) =>
        Raise(new TopicContentChangedDomainEvent(Id, Content, content));
    
    /// <inheritdoc />
    public override void Apply(object domainEvent)
    {
        switch (domainEvent)
        {
            case TopicCreatedDomainEvent e:
                (Id, Title, Content) = e;
                break;
            case TopicTitleChangedDomainEvent e:
                Title = e.NewTitle;
                break;
            case TopicContentChangedDomainEvent e:
                Content = e.NewContent;
                break;
        }
    }
}