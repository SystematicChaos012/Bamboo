using Bamboo.Users.Entities;
using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户令牌移除领域事件
    /// </summary>
    /// <param name="Id">Id</param>
    /// <param name="UserToken">令牌</param>
    public sealed record class IdentityUserTokenRemovedDomainEvent(IdentityUserId Id, IdentityUserToken Token) : DomainEvent;
}