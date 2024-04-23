using SharedKernel.Domain;

namespace Bamboo
{
    /// <summary>
    /// 文章无作者异常
    /// </summary>
    public class PostAuthorNotFoundException() : DomainException("文章无作者异常");
}