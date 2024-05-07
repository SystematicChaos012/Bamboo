using Blog.Core.Topics;
using Blog.Core.Topics.ValueObjects;

namespace Blog.Core.Tests;

/// <summary>
/// 主题测试
/// </summary>
public class TopicTests
{
    [Fact]
    public void Topic_Create()
    {
        var topicId = new TopicId(Guid.NewGuid());
        const string title = "Title";
        const string content = "Content";

        var topic = new Topic(topicId, title, content);
        
        Assert.Equal(topicId, topic.Id);
        Assert.Equal(title, topic.Title);
        Assert.Equal(content, topic.Content);
        Assert.Empty(topic.Authors);
    }
}