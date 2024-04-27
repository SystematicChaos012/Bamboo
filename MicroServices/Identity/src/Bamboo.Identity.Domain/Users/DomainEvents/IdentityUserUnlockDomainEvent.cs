using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户解锁领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    public sealed record class IdentityUserUnlockDomainEvent(IdentityUserId Id) : DomainEvent;
}