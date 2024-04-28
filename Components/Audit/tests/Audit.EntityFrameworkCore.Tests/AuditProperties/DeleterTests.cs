using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public sealed class DeleterTests : UnitTestBase
    {
        [Fact]
        public void Generate_Deleter_When_Deleted()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Context.Remove(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("Deleter"));
            Assert.Equal(Context.Entry(entity).Property("Deleter").CurrentValue, int.Parse(IdentityContext.FindClaim(Options.IdentityClaimType)!));
        }
    }
}
