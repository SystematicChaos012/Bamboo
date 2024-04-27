using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户锁定领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    public sealed record class IdentityUserLockoutDomainEvent(IdentityUserId Id, DateTimeOffset LockoutEnd) : DomainEvent;
}