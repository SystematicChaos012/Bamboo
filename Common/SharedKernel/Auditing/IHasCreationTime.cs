namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有创建时间的审计
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 名称
        /// </summary>
        static string Name => "CreationTime";
    }
}
