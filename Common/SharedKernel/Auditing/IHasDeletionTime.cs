namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有删除时间的审计
    /// </summary>
    public interface IHasDeletionTime
    {
        /// <summary>
        /// 名称
        /// </summary>
        static string Name => "DeletionTime";
    }
}
