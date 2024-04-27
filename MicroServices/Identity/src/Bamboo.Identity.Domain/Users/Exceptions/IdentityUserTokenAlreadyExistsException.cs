using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.Exceptions
{
    /// <summary>
    /// 用户令牌已存在异常
    /// </summary>
    public sealed class IdentityUserTokenAlreadyExistsException() : DomainException("令牌已存在");
}