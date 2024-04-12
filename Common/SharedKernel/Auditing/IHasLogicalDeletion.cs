namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有逻辑删除
    /// </summary>
    public interface IHasLogicalDeletion
    {
        static string Name = "IsDeleted";
    }
}
