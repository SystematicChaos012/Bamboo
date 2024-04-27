using Bamboo.Users.ValueObjects;

namespace Bamboo.Identity.Domain.Tests.Identity
{
    public class IdentityUserTests
    {
        [Fact]
        public void IdentityUser_Create()
        {
            var identityUserId = new IdentityUserId(Guid.NewGuid());
            var userName = "Username";
            var email = "test@bamboo.com";

            var identityUser = new IdentityUser(identityUserId, userName, email);

            Assert.Equal(identityUserId, identityUser.Id);
            Assert.Equal(userName, identityUser.UserName);
            Assert.Equal(email, identityUser.Email);
            Assert.Equal(identityUser.NormalizedUserName, userName.ToUpperInvariant());
            Assert.Equal(identityUser.NormalizedEmail, email.ToUpperInvariant());
        }
    }
}
