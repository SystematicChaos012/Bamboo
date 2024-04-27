using Bamboo.Users.ValueObjects;

namespace Bamboo.Users.Policies
{
    /// <summary>
    /// 用户锁定策略
    /// </summary>
    public interface IIdentityUserLockoutPolicy
    {
        /// <summary>
        /// 计算锁定时间
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns><see cref="DateTimeOffset"/></returns>
        DateTimeOffset CalcuteLockout(IdentityUserId id);
    }
}
