using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户邮箱已确认领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    public sealed record class IdentityUserEmailConfirmedDomainEvent(IdentityUserId Id) : DomainEvent;
}