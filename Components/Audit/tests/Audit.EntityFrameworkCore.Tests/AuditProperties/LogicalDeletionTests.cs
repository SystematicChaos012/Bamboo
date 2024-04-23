using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public class LogicalDeletionTests : UnitTestBase
    {
        [Fact]
        public void Generate_DeleteTime_When_Deleted()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Context.Remove(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("IsDeleted"));
            Assert.Equal(Context.Entry(entity).Property("IsDeleted").CurrentValue, true);
        }
    }
}
