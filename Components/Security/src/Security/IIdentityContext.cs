namespace Security
{
    /// <summary>
    /// 身份上下文
    /// </summary>
    public interface IIdentityContext
    {
        /// <summary>
        /// 是否已认证
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 查找声明，如果有多个相同类型的声明，只返回第一个
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>值</returns>
        string? FindClaim(string type);

        /// <summary>
        /// 查找所有声明
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>值</returns>
        IEnumerable<string> FindClaims(string type);
    }
}
