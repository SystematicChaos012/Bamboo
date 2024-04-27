using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Users.Entities
{
    /// <summary>
    /// 用户令牌
    /// </summary>
    public sealed class IdentityUserToken : Entity<IdentityUserTokenId>, IEquatable<IdentityUserToken>
    {
#nullable disable

        /// <summary>
        /// 用户 Id
        /// </summary>
        public IdentityUserId UserId { get; private set; }

        /// <summary>
        /// 登录提供者
        /// </summary>
        public string LoginProvider { get; private set; }

        /// <summary>
        /// 提供者名称
        /// </summary>
        public string Name { get; private set; }

#nullable enable

        /// <summary>
        /// 令牌值
        /// </summary>
        public string? Value { get; private set; }

        /// <summary>
        /// Used by EF Core
        /// </summary>
        private IdentityUserToken()
        {
        }

        /// <summary>
        /// 创建用户令牌
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="id">Id</param>
        /// <param name="loginProvider">登录提供者</param>
        /// <param name="name">名称</param>
        public IdentityUserToken(IdentityUserId userId, IdentityUserTokenId id, string loginProvider, string name)
        {
            Id = id;
            UserId = userId;
            LoginProvider = loginProvider;
            Name = name;
        }

        /// <summary>
        /// 更改令牌值
        /// </summary>
        /// <param name="value">令牌值</param>
        public void ChangeValue(string value)
        {
            Value = value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(LoginProvider);
            hash.Add(Name);
            return hash.ToHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as IdentityUserToken);
        }

        /// <inheritdoc/>
        public bool Equals(IdentityUserToken? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }
    }
}
