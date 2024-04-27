using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户电话号码更改领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="PhoneNumber">电话号码</param>
    public sealed record class IdentityUserPhoneNumberChangedDomainEvent(IdentityUserId Id, string PhoneNumber) : DomainEvent;
}