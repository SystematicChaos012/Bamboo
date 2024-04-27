namespace Bamboo.Users.ValueObjects
{
    /// <summary>
    /// 用户声明 Id
    /// </summary>
    /// <param name="Id">Claim Id</param>
    public readonly record struct IdentityUserClaimId(Guid Id);
}