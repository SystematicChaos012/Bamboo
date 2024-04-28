using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户锁定领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="LockoutEnd">锁定截至时间</param>
    /// <param name="AccessFailedCount">访问失败次数</param>
    public sealed record class IdentityUserLockoutDomainEvent(IdentityUserId Id, DateTimeOffset LockoutEnd, int AccessFailedCount) : DomainEvent;
}