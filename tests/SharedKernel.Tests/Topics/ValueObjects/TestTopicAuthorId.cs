namespace Blog.Core.Topics.ValueObjects;

/// <summary>
/// 主题作者 Id
/// </summary>
/// <param name="Id">主键</param>
public readonly record struct TestTopicAuthorId(Guid Id)
{
    /// <summary>
    /// 创建一个 TopicAuthorId
    /// </summary>
    public static TestTopicAuthorId NewTopicAuthorId() => From(Guid.NewGuid());

    /// <summary>
    /// 从 Guid 中创建 TopicAuthorId
    /// </summary>
    public static TestTopicAuthorId From(Guid id)
    {
        return new TestTopicAuthorId(id);
    }
}
