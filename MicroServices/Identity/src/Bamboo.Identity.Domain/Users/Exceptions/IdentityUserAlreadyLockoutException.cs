using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户已锁定异常
    /// </summary>
    public sealed class IdentityUserAlreadyLockoutException() : DomainException("用户已锁定");
}