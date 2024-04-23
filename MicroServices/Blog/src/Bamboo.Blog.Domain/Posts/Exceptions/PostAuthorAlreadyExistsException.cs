using SharedKernel.Domain;

namespace Bamboo.Posts.Exceptions
{
    /// <summary>
    /// 作者已存在异常
    /// </summary>
    public class PostAuthorAlreadyExistsException() : DomainException("作者已存在");
}
