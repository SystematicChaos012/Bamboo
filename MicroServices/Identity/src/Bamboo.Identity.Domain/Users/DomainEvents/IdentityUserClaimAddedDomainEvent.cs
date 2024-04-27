using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户声明添加领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="ClaimType">声明类型</param>
    /// <param name="ClaimValue">声明值</param>
    public sealed record class IdentityUserClaimAddedDomainEvent(IdentityUserId Id, IdentityUserClaimId ClaimId, string ClaimType, string ClaimValue) : DomainEvent;
}