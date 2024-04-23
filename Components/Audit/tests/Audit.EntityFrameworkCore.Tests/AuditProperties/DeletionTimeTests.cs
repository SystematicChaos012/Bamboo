using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public class DeletionTimeTests : UnitTestBase
    {
        [Fact]
        public void Generate_DeletionTime_When_Deleted()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Context.Remove(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("DeletionTime"));
        }
    }
}
