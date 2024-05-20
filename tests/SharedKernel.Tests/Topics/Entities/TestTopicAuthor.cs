using Blog.Core.Topics.DomainEvents;
using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.Entities;

/// <summary>
/// 主题作者
/// </summary>
public sealed class TestTopicAuthor : Entity
{
    /// <summary>
    /// 主题作者 Id
    /// </summary>
    public TestTopicAuthorId Id { get; private set; } = default!;

    /// <summary>
    /// 主题 Id
    /// </summary>
    public TestTopicId TopicId { get; private set; } = default!;

    /// <summary>
    /// 作者 Id
    /// </summary>
    public TestAuthorId AuthorId { get; private set; } = default!;

    /// <summary>
    /// 作者名称
    /// </summary>
    public string AuthorName { get; private set; } = null!;

    /// <param name="id">主键</param>
    /// <param name="topicId">主题 Id</param>
    /// <param name="authorId">作者 Id</param>
    /// <param name="authorName">作者名称</param>
    public TestTopicAuthor(TestTopicAuthorId id, TestTopicId topicId, TestAuthorId authorId, string authorName) =>
        Raise(new TopicAuthorCreatedDomainEvent(id, topicId, authorId, authorName));

    /// <summary>
    /// 创建主题作者
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="topicId">主题 Id</param>
    /// <param name="authorId">作者 Id</param>
    /// <param name="authorName">作者名称</param>
    /// <returns><see cref="TestTopicAuthor"/></returns>
    public static TestTopicAuthor Create(TestTopicAuthorId id, TestTopicId topicId, TestAuthorId authorId, string authorName) => 
        new(id, topicId, authorId, authorName);

    /// <inheritdoc/>
    public override void Apply(object domainEvent)
    {
        switch (domainEvent)
        {
            case TopicAuthorCreatedDomainEvent e:
                (Id, TopicId, AuthorId, AuthorName) = e;
                break;
        }
    }
}