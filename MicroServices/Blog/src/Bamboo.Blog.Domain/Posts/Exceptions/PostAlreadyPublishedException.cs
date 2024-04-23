using SharedKernel.Domain;

namespace Bamboo.Posts.Exceptions
{
    /// <summary>
    /// 文章已发布异常
    /// </summary>
    public class PostAlreadyPublishedException() : DomainException("文章已发布");
}