using SharedKernel.Domain;

namespace Bamboo.Users.Exceptions
{
    /// <summary>
    /// 用户声明未找到异常
    /// </summary>
    public sealed class IdentityUserClaimNotFoundException() : DomainException("用户声明未找到");
}