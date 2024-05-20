using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainExceptions;

/// <summary>
/// 主体异常帮助类
/// </summary>
public static class TopicThrowHelper
{
    public const string AuthorAddedTwiceCode = "Author_Added_Twice";
    public const string AuthorNotFoundCode = "Author_Not_Found";
    
    /// <summary>
    /// 作者重复
    /// </summary>
    [DoesNotReturn]
    public static void AuthorAddedTwice()
    {
        throw new DomainException(AuthorAddedTwiceCode, "主题作者重复");
    }

    /// <summary>
    /// 作者未找到
    /// </summary>
    /// <exception cref="DomainException"></exception>
    [DoesNotReturn]
    public static void AuthorNotFound()
    {
        throw new DomainException(AuthorNotFoundCode, "主题作者未找到");
    }
}