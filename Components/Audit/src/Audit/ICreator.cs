namespace Audit
{
    /// <summary>
    /// 创建人 (注: 不支持 ValueObject{T})
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface ICreator<TKey> where TKey : IParsable<TKey>
    {
    }
}
