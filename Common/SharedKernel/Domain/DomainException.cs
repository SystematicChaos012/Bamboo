namespace SharedKernel.Domain
{
    /// <summary>
    /// 领域异常
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// 异常代码
        /// </summary>
        public string Code { get; private set; }

        public DomainException(string code) : base()
        {
            Code = code;
        }

        public DomainException(string code, string? message) : base(message)
        {
            Code = code;
        }

        public DomainException(string code, string? message, Exception? innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
