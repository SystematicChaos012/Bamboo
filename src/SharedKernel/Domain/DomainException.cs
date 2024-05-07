namespace SharedKernel.Domain;

/// <summary>
/// 领域异常
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// 代码
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// 创建领域异常
    /// </summary>
    public DomainException(string code, string message) : base(message)
    {
        Code = code;
    }

    /// <summary>
    /// 创建领域异常
    /// </summary>
    public DomainException(string code, string message, DomainException innerException) : base(message, innerException)
    {
        Code = code;
    }
}