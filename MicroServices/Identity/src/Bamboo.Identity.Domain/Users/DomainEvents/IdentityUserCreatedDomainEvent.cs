using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户创建领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="UserName">用户名</param>
    /// <param name="Email">邮箱</param>
    public sealed record class IdentityUserCreatedDomainEvent(IdentityUserId Id, string UserName, string Email) : DomainEvent;
}
