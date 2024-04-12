namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有修改者的审计
    /// </summary>
    public interface IHasModifier<TKey>
    {
        /// <summary>
        /// 名称
        /// </summary>
        static string Name => "ModifierId";

        /// <summary>
        /// 默认值
        /// </summary>
        static TKey Default { get; set; } = default!;
    }
}
