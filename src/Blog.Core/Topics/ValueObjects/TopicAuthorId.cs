namespace Blog.Core.Topics.ValueObjects;

/// <summary>
/// 主题作者 Id
/// </summary>
/// <param name="Id">主键</param>
public readonly record struct TopicAuthorId(Guid Id)
{
    /// <summary>
    /// 从 Guid 中创建 TopicAuthorId
    /// </summary>
    public static TopicAuthorId From(Guid id)
    {
        return new TopicAuthorId(id);
    }
}
