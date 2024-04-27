using Audit;
using Bamboo.Users.DomainEvents;
using Bamboo.Users.Exceptions;
using Bamboo.Users.Policies;
using Bamboo.Users.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public sealed partial class IdentityUser
        : AggregateRoot<IdentityUserId>
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

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="userName">用户名</param>
        /// <param name="email">邮箱</param>
        public IdentityUser(IdentityUserId id, string userName, string email)
        {
            RaiseEvent(new IdentityUserCreatedDomainEvent(id, userName, email));
        }

        /// <summary>
        /// 更改用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        public void ChangeUserName(string userName)
        {
            RaiseEvent(new IdentityUserUserNameChangedDomainEvent(Id, userName));
        }

        /// <summary>
        /// 更改邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        public void ChangeEmail(string email)
        {
            RaiseEvent(new IdentityUserEmailChangedDomainEvent(Id, email));
        }

        /// <summary>
        /// 邮箱确认
        /// </summary>
        /// <exception cref="IdentityUserEmailAlreadyConfirmedException">邮箱已确认异常</exception>
        public void EmailConfirm()
        {
            if (EmailConfirmed)
            {
                throw new IdentityUserEmailAlreadyConfirmedException();
            }

            RaiseEvent(new IdentityUserEmailConfirmedDomainEvent(Id));
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="passwordHash">密码</param>
        public void ChangePassword(string passwordHash)
        {
            RaiseEvent(new IdentityUserPasswordChangedDomainEvent(Id, passwordHash));
            ChangeSecurityStamp();
        }

        /// <summary>
        /// 更改安全戳
        /// </summary>
        public void ChangeSecurityStamp()
        {
            RaiseEvent(new IdentityUserSecurityStampChangedDomainEvent(Id, Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// 更改电话号码
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        public void ChangePhoneNumber(string phoneNumber)
        {
            RaiseEvent(new IdentityUserPhoneNumberChangedDomainEvent(Id, phoneNumber));
        }

        /// <summary>
        /// 电话号码确认
        /// </summary>
        /// <exception cref="IdentityUserPhoneNumberAlreadyConfirmedException">电话已存在异常</exception>
        public void PhoneNumberConfirm()
        {
            if (PhoneNumberConfirmed)
            {
                throw new IdentityUserPhoneNumberAlreadyConfirmedException();
            }

            RaiseEvent(new IdentityUserPhoneNumberConfirmedDomainEvent(Id));
        }

        /// <summary>
        /// 用户锁定
        /// </summary>
        /// <exception cref="IdentityUserAlreadyLockoutException">用户已锁定异常</exception>
        public void Lockout(IIdentityUserLockoutPolicy lockoutPolicy)
        {
            if (LockoutEnabled)
            {
                throw new IdentityUserAlreadyLockoutException();
            }

            var lockoutEnd = lockoutPolicy.CalcuteLockout(Id);

            RaiseEvent(new IdentityUserLockoutDomainEvent(Id, lockoutEnd));
        }

        /// <summary>
        /// 用户解锁
        /// </summary>
        public void Unlock()
        {
            if (LockoutEnabled is not true)
            {
                throw new IdentityUserNotLockoutException();
            }

            RaiseEvent(new IdentityUserUnlockDomainEvent(Id));
        }

        /// <summary>
        /// 访问失败
        /// </summary>
        public void AccessFail()
        {
            RaiseEvent(new IdentityUserAccessFailedDomainEvent(Id));
        }
    }

    partial class IdentityUser 
        : IDomainEventApplier<IdentityUserCreatedDomainEvent>
        , IDomainEventApplier<IdentityUserUserNameChangedDomainEvent>
        , IDomainEventApplier<IdentityUserEmailChangedDomainEvent>
        , IDomainEventApplier<IdentityUserEmailConfirmedDomainEvent>
        , IDomainEventApplier<IdentityUserPhoneNumberChangedDomainEvent>
        , IDomainEventApplier<IdentityUserPhoneNumberConfirmedDomainEvent>
        , IDomainEventApplier<IdentityUserAccessFailedDomainEvent>
        , IDomainEventApplier<IdentityUserLockoutDomainEvent>
        , IDomainEventApplier<IdentityUserUnlockDomainEvent>
        , IDomainEventApplier<IdentityUserPasswordChangedDomainEvent>
        , IDomainEventApplier<IdentityUserSecurityStampChangedDomainEvent>
    {
        void IDomainEventApplier<IdentityUserCreatedDomainEvent>.Apply(IdentityUserCreatedDomainEvent domainEvent)
        {
            (Id, UserName, Email) = domainEvent;
            (NormalizedUserName, NormalizedEmail) = (UserName.ToUpperInvariant(), Email.ToUpperInvariant());
        }

        void IDomainEventApplier<IdentityUserUserNameChangedDomainEvent>.Apply(IdentityUserUserNameChangedDomainEvent domainEvent)
        {
            UserName = domainEvent.UserName;
            NormalizedUserName = domainEvent.UserName.ToUpperInvariant();
        }

        void IDomainEventApplier<IdentityUserEmailChangedDomainEvent>.Apply(IdentityUserEmailChangedDomainEvent domainEvent)
        {
            Email = domainEvent.Email;
            NormalizedEmail = domainEvent.Email.ToUpperInvariant();
            EmailConfirmed = false;
        }

        void IDomainEventApplier<IdentityUserEmailConfirmedDomainEvent>.Apply(IdentityUserEmailConfirmedDomainEvent domainEvent)
        {
            EmailConfirmed = true;
        }

        void IDomainEventApplier<IdentityUserPhoneNumberChangedDomainEvent>.Apply(IdentityUserPhoneNumberChangedDomainEvent domainEvent)
        {
            PhoneNumber = domainEvent.PhoneNumber;
            PhoneNumberConfirmed = false;
        }

        void IDomainEventApplier<IdentityUserPhoneNumberConfirmedDomainEvent>.Apply(IdentityUserPhoneNumberConfirmedDomainEvent domainEvent)
        {
            PhoneNumberConfirmed = true;
        }

        void IDomainEventApplier<IdentityUserAccessFailedDomainEvent>.Apply(IdentityUserAccessFailedDomainEvent domainEvent)
        {
            AccessFailedCount += 1;
        }

        void IDomainEventApplier<IdentityUserLockoutDomainEvent>.Apply(IdentityUserLockoutDomainEvent domainEvent)
        {
            LockoutEnabled = true;
            LockoutEnd = domainEvent.LockoutEnd;
        }

        void IDomainEventApplier<IdentityUserUnlockDomainEvent>.Apply(IdentityUserUnlockDomainEvent domainEvent)
        {
            LockoutEnabled = false;
            LockoutEnd = null;
        }

        void IDomainEventApplier<IdentityUserPasswordChangedDomainEvent>.Apply(IdentityUserPasswordChangedDomainEvent domainEvent)
        {
            PasswordHash = domainEvent.PasswordHash;
        }

        void IDomainEventApplier<IdentityUserSecurityStampChangedDomainEvent>.Apply(IdentityUserSecurityStampChangedDomainEvent domainEvent)
        {
            SecurityStamp = domainEvent.SecurityStamp;
        }
    }
}