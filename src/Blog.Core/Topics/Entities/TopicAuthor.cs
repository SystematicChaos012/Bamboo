using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.Entities;

/// <summary>
/// 主题作者
/// </summary>
public sealed class TopicAuthor : Entity
{
    /// <summary>
    /// 主键
    /// </summary>
    public TopicAuthorId Id { get; private set; }

    /// <summary>
    /// 主题 Id
    /// </summary>
    public TopicId TopicId { get; private set; }

    /// <summary>
    /// 用户 Id
    /// </summary>
    public AuthorId AuthorId { get; private set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string AuthorName { get; private set; }

    /// <summary>
    /// 主题
    /// </summary>
    public Topic Topic { get; } = null!;
    
    /// <summary>
    /// 创建主题作者
    /// </summary>
    /// <param name="topicId">主题 Id</param>
    /// <param name="topicAuthorId">主键</param>
    /// <param name="authorId">用户 Id</param>
    /// <param name="authorName">用户名称</param>
    public TopicAuthor(TopicId topicId, TopicAuthorId topicAuthorId, AuthorId authorId, string authorName)
    {
        (Id, TopicId, AuthorId, AuthorName) = (topicAuthorId, topicId, authorId, authorName);
    }

    /// <inheritdoc />
    public override void Raise<T>(T domainEvent) => Topic.Raise(domainEvent);
}