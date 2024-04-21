namespace Audit
{
    /// <summary>
    /// 创建人
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface ICreator<TKey> where TKey : IParsable<TKey>
    {
    }
}
