using SharedKernel.Domain;

namespace Bamboo.Users.Exceptions
{
    /// <summary>
    /// 用户令牌未找到异常
    /// </summary>
    public sealed class IdentityUserTokenNotFoundException() : DomainException("令牌不存在");
}