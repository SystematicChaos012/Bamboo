using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public class CreatorWithoutAuthTests : UnitTestWithoutAuthBase
    {
        [Fact]
        public void Generate_Creator_When_Added()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("Creator"));
            Assert.Null(Context.Entry(entity).Property("Creator").CurrentValue);
        }
    }
}
