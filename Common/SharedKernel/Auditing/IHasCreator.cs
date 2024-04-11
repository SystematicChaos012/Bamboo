namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有创建者的审计
    /// </summary>
    public interface IHasCreator<T, TKey>
    {
        /// <summary>
        /// 名称
        /// </summary>
        static string Name => "CreatorId";

        /// <summary>
        /// 默认值
        /// </summary>
        static TKey Default { get; set; } = default!;
    }
}
