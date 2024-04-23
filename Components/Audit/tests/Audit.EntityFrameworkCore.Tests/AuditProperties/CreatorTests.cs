using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public class CreatorTests : UnitTestBase
    {
        [Fact]
        public void Generate_Creator_When_Added()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("Creator"));
            Assert.Equal(Context.Entry(entity).Property("Creator").CurrentValue, int.Parse(CurrentUser.Id));
        }
    }
}
