using SharedKernel.Domain;

namespace Bamboo.Posts.Exceptions
{
    /// <summary>
    /// 文章无作者异常
    /// </summary>
    public sealed class PostAuthorNotFoundException() : DomainException("文章无作者异常");
}