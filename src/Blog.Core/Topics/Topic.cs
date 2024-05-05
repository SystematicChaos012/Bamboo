using Blog.Core.Topics.DomainEvents;
using Blog.Core.Topics.Entities;
using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics;

/// <summary>
/// 主题
/// </summary>
public sealed partial class Topic : AggregateRoot
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
    /// 主题作者
    /// </summary>
    public ICollection<TopicAuthor> Authors { get; private set; } = [];

    /// <summary>
    /// 创建主题
    /// </summary>
    /// <param name="id">主键 Id</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    public Topic(TopicId id, string title, string content) => 
        Raise(new TopicCreatedDomainEvent(id, title, content));

    /// <summary>
    /// 添加作者
    /// </summary>
    /// <param name="topicAuthorId">主键 Id</param>
    /// <param name="identityUserId">用户 Id</param>
    /// <param name="identityUserName">用户名称</param>
    public void AddAuthor(TopicAuthorId topicAuthorId, IdentityUserId identityUserId, string identityUserName)
    {
        // 作者已存在
        if (Authors.Any(x => x.IdentityUserId == identityUserId))
        {
            return;
        }

        Raise(new TopicAuthorAddedDomainEvent(Id, topicAuthorId, identityUserId, identityUserName));
    }
}