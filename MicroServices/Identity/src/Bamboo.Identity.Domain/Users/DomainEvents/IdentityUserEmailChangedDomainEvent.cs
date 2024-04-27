using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户邮箱已更改领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="Email">邮箱</param>
    public sealed record class IdentityUserEmailChangedDomainEvent(IdentityUserId Id, string Email) : DomainEvent;
}