namespace Bamboo.Users.Exceptions
{
    /// <summary>
    /// 用户未锁定异常
    /// </summary>
    public sealed class IdentityUserNotLockoutException() : Exception("用户未锁定");
}