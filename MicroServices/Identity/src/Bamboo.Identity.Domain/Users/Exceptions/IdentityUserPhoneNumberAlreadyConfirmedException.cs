using SharedKernel.Domain;

namespace Bamboo.Users.Exceptions
{
    /// <summary>
    /// 用户电话号码已确认异常
    /// </summary>
    public sealed class IdentityUserPhoneNumberAlreadyConfirmedException() : DomainException("用户电话号码已确认");
}