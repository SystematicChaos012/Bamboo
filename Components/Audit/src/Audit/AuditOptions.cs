namespace Audit
{
    /// <summary>
    /// 审计选项
    /// </summary>
    public class AuditOptions
    {
        /// <summary>
        /// 身份声明类型
        /// </summary>
        public string IdentityClaimType { get; set; } = "sub";
    }
}
