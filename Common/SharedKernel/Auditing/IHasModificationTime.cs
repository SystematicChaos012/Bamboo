namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有修改时间的审计
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        /// 名称
        /// </summary>
        static string Name => "ModificationTime";
    }
}
