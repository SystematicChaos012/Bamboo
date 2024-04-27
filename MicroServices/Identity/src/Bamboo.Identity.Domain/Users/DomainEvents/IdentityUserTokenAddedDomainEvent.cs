using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户令牌添加领域事件
    /// </summary>
    /// <param name="Id">Id</param>
    /// <param name="TokenId">令牌 Id</param>
    /// <param name="LoginProvider">登录提供者</param>
    /// <param name="Name">名称</param>
    public sealed record class IdentityUserTokenAddedDomainEvent(IdentityUserId Id, IdentityUserTokenId TokenId, string LoginProvider, string Name) : DomainEvent;
}