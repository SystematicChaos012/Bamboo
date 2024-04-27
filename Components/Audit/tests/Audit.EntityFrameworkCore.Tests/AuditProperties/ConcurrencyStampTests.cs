using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public sealed class ConcurrencyStampTests : UnitTestBase
    {
        [Fact]
        public void Generate_ConcurrencyStamp_When_Added()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("ConcurrencyStamp"));
        }

        [Fact]
        public void Generate_ConcurrencyStamp_When_Updated()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();
            var orignalValue = Context.Entry(entity).Property("ConcurrencyStamp").CurrentValue;

            entity.Name = "Updated";
            Context.SaveChanges();
            var currentValue = Context.Entry(entity).Property("ConcurrencyStamp").CurrentValue;

            Assert.NotEqual(orignalValue, currentValue);
        }
    }
}
