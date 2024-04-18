namespace SharedKernel.Identity
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// 是否认证
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 是否具有指定的权限
        /// </summary>
        bool HasPermission(string permissionName);
    }
}
