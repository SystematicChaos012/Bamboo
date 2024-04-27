using Bamboo.Users.Entities;
using Bamboo.Users.Exceptions;
using Bamboo.Users.Policies;
using Bamboo.Users.ValueObjects;
using System.Security.Cryptography.X509Certificates;

namespace Bamboo.Identity.Domain.Tests.Identity
{
    public sealed class IdentityUserTests
    {
        [Fact]
        public void IdentityUser_Create()
        {
            var identityUserId = new IdentityUserId(Guid.NewGuid());
            var userName = "Username";
            var email = "test@bamboo.com";
            var normalizedUserName = userName.ToUpperInvariant();
            var normalizedEmail = email.ToUpperInvariant();

            var identityUser = new IdentityUser(identityUserId, userName, email);

            Assert.Equal(identityUserId, identityUser.Id);
            Assert.Equal(userName, identityUser.UserName);
            Assert.Equal(normalizedUserName, identityUser.NormalizedUserName);
            Assert.Equal(email, identityUser.Email);
            Assert.Equal(normalizedEmail, identityUser.NormalizedEmail);
            Assert.False(identityUser.EmailConfirmed);
            Assert.Null(identityUser.PasswordHash);
            Assert.Null(identityUser.SecurityStamp);
            Assert.Null(identityUser.PhoneNumber);
            Assert.False(identityUser.PhoneNumberConfirmed);
            Assert.False(identityUser.TwoFactorEnabled);
            Assert.Null(identityUser.LockoutEnd);
            Assert.False(identityUser.LockoutEnabled);
            Assert.Equal(0, identityUser.AccessFailedCount);
        }

        [Fact]
        public void IdentityUser_ChangeUserName()
        {
            var identityUser = CreateIdentityUser();
            var userName = "bob";
            var normalizedUserName = userName.ToUpperInvariant();

            identityUser.ChangeUserName(userName);

            Assert.Equal(userName, identityUser.UserName);
            Assert.Equal(normalizedUserName, identityUser.NormalizedUserName);
        }

        [Fact]
        public void IdentityUser_ChangeEmail()
        {
            var identityUser = CreateIdentityUser();
            var email = "bob@bamboo.com";
            var normalizedEmail = email.ToUpperInvariant();

            identityUser.ChangeEmail(email);

            Assert.Equal(email, identityUser.Email);
            Assert.Equal(normalizedEmail, identityUser.NormalizedEmail);
            Assert.False(identityUser.EmailConfirmed);
        }

        [Fact]
        public void IdentityUser_EmailConfirm()
        {
            var identityUser = CreateIdentityUser();

            identityUser.EmailConfirm();

            Assert.True(identityUser.EmailConfirmed);
        }

        [Fact]
        public void IdentityUser_EmailConfirm_When_EmailConfirmed()
        {
            var identityUser = Create();

            Assert.Throws<IdentityUserEmailAlreadyConfirmedException>(identityUser.EmailConfirm);

            static IdentityUser Create()
            {
                var t = CreateIdentityUser();
                t.EmailConfirm();
                return t;
            }
        }

        [Fact]
        public void IdentityUser_ChangePhoneNumber()
        {
            var identityUser = CreateIdentityUser();
            var phoneNumber = "123456789";

            identityUser.ChangePhoneNumber(phoneNumber);

            Assert.Equal(phoneNumber, identityUser.PhoneNumber);
            Assert.False(identityUser.PhoneNumberConfirmed);
        }

        [Fact]
        public void IdentityUser_PhoneNumberConfirm()
        {
            var identityUser = Create();

            identityUser.PhoneNumberConfirm();

            Assert.True(identityUser.PhoneNumberConfirmed);

            static IdentityUser Create()
            {
                var t = CreateIdentityUser();
                t.ChangePhoneNumber("123456789");
                return t;
            }
        }

        [Fact]
        public void IdentityUser_PhoneNumberConfirm_When_PhoneNumberConfirmed()
        {
            var identityUser = Create();

            Assert.Throws<IdentityUserPhoneNumberAlreadyConfirmedException>(identityUser.PhoneNumberConfirm);

            static IdentityUser Create()
            {
                var t = CreateIdentityUser();
                t.ChangePhoneNumber("123456789");
                t.PhoneNumberConfirm();
                return t;
            }
        }

        [Fact]
        public void IdentityUser_AccessFail()
        {
            var identityUser = CreateIdentityUser();

            identityUser.AccessFail();

            Assert.Equal(1, identityUser.AccessFailedCount);
        }

        [Fact]
        public void IdentityUser_Lockout()
        {
            var identityUser = CreateIdentityUser();

            identityUser.Lockout(FakeIdentityUserLockoutPolicy.Instance);

            Assert.True(identityUser.LockoutEnabled);
            Assert.NotNull(identityUser.LockoutEnd);
        }

        [Fact]
        public void IdentityUser_Lockout_When_LockoutEnabled()
        {
            var identityUser = Create();

            Assert.Throws<IdentityUserAlreadyLockoutException>(() => identityUser.Lockout(FakeIdentityUserLockoutPolicy.Instance));

            static IdentityUser Create()
            {
                var t = CreateIdentityUser();
                t.Lockout(FakeIdentityUserLockoutPolicy.Instance);
                return t;
            }
        }

        [Fact]
        public void IdentityUser_Unlock()
        {
            var identityUser = Create();

            identityUser.Unlock();

            Assert.False(identityUser.LockoutEnabled);
            Assert.Null(identityUser.LockoutEnd);

            static IdentityUser Create()
            {
                var t = CreateIdentityUser();
                t.Lockout(FakeIdentityUserLockoutPolicy.Instance);
                return t;
            }
        }

        [Fact]
        public void IdentityUser_Unlock_When_NotLockout()
        {
            var identityUser = CreateIdentityUser();

            Assert.Throws<IdentityUserNotLockoutException>(identityUser.Unlock);
        }

        [Fact]
        public void IdentityUser_ChangePassword()
        {
            var identityUser = CreateIdentityUser();
            var passwordHash = "passwordHash";

            identityUser.ChangePassword(passwordHash);

            Assert.Equal(passwordHash, identityUser.PasswordHash);
            Assert.NotNull(identityUser.SecurityStamp);
        }

        [Fact]
        public void IdentityUser_ChangeSecurityStamp()
        {
            var identityUser = CreateIdentityUser();

            identityUser.ChangeSecurityStamp();

            Assert.NotNull(identityUser.SecurityStamp);
        }

        [Fact]
        public void IdentityUser_AddClaim()
        {
            var identityUser = CreateIdentityUser();
            var claimId = new IdentityUserClaimId(Guid.NewGuid());
            var claimType = "claimType";
            var claimValue = "claimValue";

            identityUser.AddClaim(claimId, claimType, claimValue);

            Assert.Contains(identityUser.Claims, x => x.Id == claimId && x.ClaimType == claimType && x.ClaimValue == claimValue);
        }

        [Fact]
        public void IdentityUser_RemoveClaim()
        {
            var identityUser = Create(out var claim);

            identityUser.RemoveClaim(claim.Id);

            Assert.Empty(identityUser.Claims);

            static IdentityUser Create(out IdentityUserClaim claim)
            {
                var t = CreateIdentityUser();
                t.AddClaim(new IdentityUserClaimId(Guid.NewGuid()), "claimType", "claimValue");

                claim = t.Claims.First();

                return t;
            }
        }

        [Fact]
        public void IdentityUser_RemoveClaim_When_NotFound()
        {
            var identityUser = CreateIdentityUser();

            Assert.Throws<IdentityUserClaimNotFoundException>(() => identityUser.RemoveClaim(new IdentityUserClaimId(Guid.NewGuid())));
        }

        private static IdentityUser CreateIdentityUser()
        {
            return new IdentityUser(
                new IdentityUserId(Guid.NewGuid()),
                "alice",
                "alice@bamboo.com");
        }

        class FakeIdentityUserLockoutPolicy : IIdentityUserLockoutPolicy
        {
            public static FakeIdentityUserLockoutPolicy Instance => new FakeIdentityUserLockoutPolicy();

            public DateTimeOffset CalcuteLockout(IdentityUserId id)
            {
                return DateTimeOffset.UtcNow.AddMinutes(5);
            }
        }
    }
}
