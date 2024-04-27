using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户安全戳更改领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="SecurityStamp">安全戳</param>
    public sealed record class IdentityUserSecurityStampChangedDomainEvent(IdentityUserId Id, string SecurityStamp) : DomainEvent, IDomainEventIgnorePersistent;
}