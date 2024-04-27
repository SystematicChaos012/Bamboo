using SharedKernel.Domain;

namespace Bamboo.Users.Exceptions
{
    /// <summary>
    /// 用户邮箱已确认异常
    /// </summary>
    public sealed class IdentityUserEmailAlreadyConfirmedException() : DomainException("用户邮箱已确认");
}