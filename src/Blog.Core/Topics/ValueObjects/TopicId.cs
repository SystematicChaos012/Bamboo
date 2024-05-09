namespace Blog.Core.Topics.ValueObjects;

/// <summary>
/// 主题 Id
/// </summary>
/// <param name="Id">主键</param>
public readonly record struct TopicId(Guid Id)
{
    /// <summary>
    /// 从 Guid 中创建 TopicId
    /// </summary>
    public static TopicId From(Guid id)
    {
        return new TopicId(id);
    }
}