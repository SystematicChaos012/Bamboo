using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户手机号码已确认领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    public sealed record class IdentityUserPhoneNumberConfirmedDomainEvent(IdentityUserId Id) : DomainEvent;
}