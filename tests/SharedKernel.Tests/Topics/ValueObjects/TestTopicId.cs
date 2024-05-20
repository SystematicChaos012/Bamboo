namespace Blog.Core.Topics.ValueObjects;

/// <summary>
/// 主题 Id
/// </summary>
/// <param name="Id">主键</param>
public readonly record struct TestTopicId(Guid Id)
{
    /// <summary>
    /// 创建一个 TopicId
    /// </summary>
    public static TestTopicId NewTopicId() => From(Guid.NewGuid());

    /// <summary>
    /// 从 Guid 中创建 TopicId
    /// </summary>
    public static TestTopicId From(Guid id)
    {
        return new TestTopicId(id);
    }
}