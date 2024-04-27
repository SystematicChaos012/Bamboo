using Audit;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class IdentityUser
        : AggregateRoot<Guid>
        , IConcurrencyStamp, ICreationTime, ICreator<Guid>, IModificationTime, IModifier<Guid>, IDeletionTime, IDeleter<Guid>, ILogicalDeletion
    {
#nullable disable

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// 标准化用户名
        /// </summary>
        public string NormalizedUserName { get; private set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// 标准化用户名
        /// </summary>
        public string NormalizedEmail { get; private set; }

        /// <summary>
        /// 邮箱是否已确认
        /// </summary>
        public bool EmailConfirmed { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// 每当用户凭据更改（密码更改、登录删除）时都必须更改的随机值
        /// </summary>
        public string SecurityStamp { get; private set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// 电话号码是否已确认
        /// </summary>
        public bool PhoneNumberConfirmed { get; private set; }

        /// <summary>
        /// 是否为此用户启用了双因素身份验证
        /// </summary>
        public bool TwoFactorEnabled { get; private set; }

        /// <summary>
        /// 锁定结束时间
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; private set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool LockoutEnabled { get; private set; }

        /// <summary>
        /// 访问失败次数
        /// </summary>
        public int AccessFailedCount { get; private set; }

#nullable enable

        /// <summary>
        /// Used by EF Core
        /// </summary>
        private IdentityUser() { }
    }
}