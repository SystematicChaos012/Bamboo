using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.DomainEvents
{
    /// <summary>
    /// 用户登录失败事件
    /// </summary>
    /// <param name="Id">User Id</param>
    public sealed record class IdentityUserAccessFailedDomainEvent(IdentityUserId Id) : DomainEvent;
}