using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户密码更改领域事件
    /// </summary>
    /// <param name="Id">User Id</param>
    /// <param name="PasswordHash">密码</param>
    public sealed record class IdentityUserPasswordChangedDomainEvent(IdentityUserId Id, string PasswordHash) : DomainEvent;

    /*
     * 1. 领域事件如果被持久化，意味着密码也被持久化，如果被加以利用或者数据库泄露会导致密码泄露
     * 2. 所以这边可能需要通过某种手段让密码不被持久化，但是如果不持久化密码，那么事件溯源将丢失密码信息
     * 3. 为了解决上面问题，应该在持久化前先对密码进行加盐 Hash 等操作，让密码变成本来已经不可读的状态
     * 4. 这边应该禁止在后续的 DomainEventHandler 中再去密码加盐 Hash
     */
}