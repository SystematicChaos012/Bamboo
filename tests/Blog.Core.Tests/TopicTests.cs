using Blog.Core.Topics;
using Blog.Core.Topics.ValueObjects;

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
}