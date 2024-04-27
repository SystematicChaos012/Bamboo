namespace Bamboo.Users.ValueObjects
{
    /// <summary>
    /// 用户 Id
    /// </summary>
    /// <param name="Id">User Id</param>
    public readonly record struct IdentityUserId(Guid Id);
}
