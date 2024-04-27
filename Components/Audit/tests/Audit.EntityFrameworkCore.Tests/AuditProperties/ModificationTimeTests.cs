using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public sealed class ModificationTimeTests : UnitTestBase
    {
        [Fact]
        public void Generate_ModificationTime_When_Modified()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            entity.Name = "New Name";
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("ModificationTime"));
        }
    }
}
