using Microsoft.AspNetCore.Identity;

namespace Bamboo.Identity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class IdentityUser
    {
        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        [PersonalData]
        public Guid Id { get; }

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        [ProtectedPersonalData]
        public string? UserName { get; private set; }

        /// <summary>
        /// Gets or sets the normalized user name for this user.
        /// </summary>
        public string? NormalizedUserName { get; private set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        [ProtectedPersonalData]
        public string? Email { get; private set; }

        /// <summary>
        /// Gets or sets the normalized email address for this user.
        /// </summary>
        public string? NormalizedEmail { get; private set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        [PersonalData]
        public bool EmailConfirmed { get; private set; }

        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public string? PasswordHash { get; private set; }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password
        /// changed, login removed)
        /// </summary>
        public string? SecurityStamp { get; private set; }

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store
        /// </summary>
        public string? ConcurrencyStamp { get; private set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        [ProtectedPersonalData]
        public string? PhoneNumber { get; private set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        [PersonalData]
        public bool PhoneNumberConfirmed { get; private set; }

        /// <summary>
        /// Gets or sets a flag indicating if two factor authentication is enabled for this
        /// user.
        /// </summary>
        [PersonalData]
        public bool TwoFactorEnabled { get; private set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; private set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        public bool LockoutEnabled { get; private set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        public int AccessFailedCount { get; private set; }

        /// <summary>
        /// Initializes
        /// </summary>
        private IdentityUser() { }
    }
}
