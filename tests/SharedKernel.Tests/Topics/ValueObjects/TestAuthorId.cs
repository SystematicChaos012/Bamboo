namespace Blog.Core.Topics.ValueObjects;

/// <summary>
/// 用户 Id
/// </summary>
/// <param name="Id">主键</param>
public readonly record struct TestAuthorId(Guid Id)
{
    /// <summary>
    /// 创建一个 AuthorId
    /// </summary>
    public static TestAuthorId NewAuthorId() => From(Guid.NewGuid());

    /// <summary>
    /// 从 Guid 中创建 IdentityUserId
    /// </summary>
    public static TestAuthorId From(Guid id)
    {
        return new TestAuthorId(id);
    }
}