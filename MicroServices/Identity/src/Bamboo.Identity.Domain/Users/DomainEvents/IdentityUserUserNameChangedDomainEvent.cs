using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户名更改领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="UserName">用户名</param>
    public sealed record class IdentityUserUserNameChangedDomainEvent(IdentityUserId Id, string UserName) : DomainEvent;
}