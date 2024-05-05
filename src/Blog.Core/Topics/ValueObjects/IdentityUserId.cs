namespace Blog.Core.Topics.ValueObjects;

/// <summary>
/// 用户 Id
/// </summary>
/// <param name="Id">主键</param>
public readonly record struct IdentityUserId(Guid Id);