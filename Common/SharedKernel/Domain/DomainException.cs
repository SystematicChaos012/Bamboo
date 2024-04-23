namespace SharedKernel.Domain
{
    /// <summary>
    /// 领域异常
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() : base()
        {
        }

        public DomainException(string? message) : base(message)
        {
        }

        public DomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
