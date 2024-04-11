namespace SharedKernel.Auditing
{
    /// <summary>
    /// 具有删除者的审计
    /// </summary>
    public interface IHasDeleter<T, TKey>
    {
        /// <summary>
        /// 名称
        /// </summary>
        static string Name => "DeleterId";

        /// <summary>
        /// 默认值
        /// </summary>
        static TKey Default { get; set; } = default!;
    }
}
