using Bamboo.Users.Entities;
using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户声明已移除领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="Claim">声明</param>
    public sealed record class IdentityUserClaimRemovedDomainEvent(IdentityUserId Id, IdentityUserClaim Claim) : DomainEvent;
}