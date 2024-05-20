namespace SharedKernel.Generators
{
    /// <summary>
    /// 雪花 Id 生成器
    /// </summary>
    public interface ISnowflakeIdGenerator
    {
        /// <summary>
        /// 生成雪花 Id
        /// </summary>
        Guid Generate();
    }
}
