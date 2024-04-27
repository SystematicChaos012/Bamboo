using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;
using System.Security.Claims;

namespace Bamboo.Users.Entities
{
    /// <summary>
    /// 用户声明
    /// </summary>
    public sealed class IdentityUserClaim : Entity<IdentityUserClaimId>, IEquatable<IdentityUserClaim>
    {
#nullable disable

        /// <summary>
        /// 用户 Id
        /// </summary>
        public IdentityUserId UserId { get; private set; }

        /// <summary>
        /// 声明类型
        /// </summary>
        public string ClaimType { get; private set; }

        /// <summary>
        /// 声明值
        /// </summary>
        public string ClaimValue { get; private set; }

#nullable enable

        /// <summary>
        /// Used by EF Core
        /// </summary>
        private IdentityUserClaim()
        {
        }

        /// <summary>
        /// 创建用户声明
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="id">Id</param>
        /// <param name="claimType">声明类型</param>
        /// <param name="claimValue">声明值</param>
        public IdentityUserClaim(IdentityUserId userId, IdentityUserClaimId id, string claimType, string claimValue)
        {
            Id = id;
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        /// <summary>
        /// 更改值
        /// </summary>
        /// <param name="claimValue">声明值</param>
        public void ChangeValue(string claimValue)
        {
            ClaimValue = claimValue;
        }

        /// <summary>
        /// 转换成 Claim
        /// </summary>
        /// <returns><see cref="Claim"/></returns>
        public Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(UserId);
            hash.Add(ClaimType);
            hash.Add(ClaimValue);
            return hash.ToHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return Equals(obj as IdentityUserClaim);
        }

        /// <inheritdoc/>
        public bool Equals(IdentityUserClaim? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserId == other.UserId;
        }
    }
}
