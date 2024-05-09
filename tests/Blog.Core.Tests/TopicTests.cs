using Blog.Core.Topics;
using Blog.Core.Topics.DomainExceptions;
using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Tests;

/// <summary>
/// 主题测试
/// </summary>
public sealed class TopicTests
{
    [Fact]
    public void Topic_Create()
    {
        var topicId = TopicId.From(Guid.NewGuid());
        const string title = "Title";
        const string content = "Content";

        var topic = new Topic(topicId, title, content);

        Assert.Equal(topicId, topic.Id);
        Assert.Equal(title, topic.Title);
        Assert.Equal(content, topic.Content);
        Assert.Empty(topic.Authors);
    }

    [Fact]
    public void Topic_Add_Author()
    {
        var topic = CreateTopic();
        var topicAuthorId = TopicAuthorId.From(Guid.NewGuid());
        var authorId = AuthorId.From(Guid.NewGuid());
        const string authorName = "This is name";

        topic.AddAuthor(topicAuthorId, authorId, authorName);
        var author = topic.Authors.FirstOrDefault();

        Assert.NotNull(author);
        Assert.Single(topic.Authors);
        Assert.Equal(topicAuthorId, author.Id);
        Assert.Equal(author.TopicId, author.TopicId);
        Assert.Equal(authorId, author.AuthorId);
        Assert.Equal(authorName, author.AuthorName);
    }

    [Fact]
    public void Topic_Add_Author_Twice()
    {
        var topic = CreateTopic();
        var topicAuthorId = TopicAuthorId.From(Guid.NewGuid());
        var authorId = AuthorId.From(Guid.NewGuid());
        const string authorName = "Author";

        topic.AddAuthor(topicAuthorId, authorId, authorName);

        var exception = Assert.Throws<DomainException>(() => 
            topic.AddAuthor(topicAuthorId, authorId, authorName));
        Assert.Equal(TopicThrowHelper.AuthorAddedTwiceCode, exception.Code);
    }

    [Fact]
    public void Topic_Remove_Author()
    {
        var topic = CreateTopicWithAuthor(out var topicAuthorId);

        topic.RemoveAuthor(topicAuthorId);

        Assert.Empty(topic.Authors);
    }

    [Fact]
    public void Topic_Remove_Author_But_Not_Exists()
    {
        var topic = CreateTopicWithAuthor(out _);

        var exception = Assert.Throws<DomainException>(() =>
            topic.RemoveAuthor(TopicAuthorId.From(Guid.Empty)));
        Assert.Equal(TopicThrowHelper.AuthorNotFoundCode, exception.Code);
    }

    /// <summary>
    /// 创建主题
    /// </summary>
    private static Topic CreateTopic()
    {
        var topicId = TopicId.From(Guid.NewGuid());
        const string title = "Title";
        const string content = "Content";

        var topic = new Topic(topicId, title, content);

        return topic;
    }

    /// <summary>
    /// 创建带有作者的主题
    /// </summary>
    private static Topic CreateTopicWithAuthor(out TopicAuthorId topicAuthorId)
    {
        var topic = CreateTopic();
        topicAuthorId = TopicAuthorId.From(Guid.NewGuid());
        var authorId = AuthorId.From(Guid.NewGuid());
        const string authorName = "Author";

        topic.AddAuthor(topicAuthorId, authorId, authorName);

        return topic;
    }
}