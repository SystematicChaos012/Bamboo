using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public sealed class CreationTimeTests : UnitTestBase
    {
        [Fact]
        public void Generate_CreateTime_When_Added()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("CreationTime"));
        }
    }
}
