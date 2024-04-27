using Audit.EntityFrameworkCore;

namespace Audit.AuditProperties
{
    public sealed class ModifierTests : UnitTestBase
    {
        [Fact]
        public void Generate_Modifier_When_Modified()
        {
            var entity = new MyEntity();

            Context.Add(entity);
            Context.SaveChanges();

            entity.Name = "New Name";
            Context.SaveChanges();

            Assert.NotNull(Context.Entry(entity).Property("Modifier"));
            Assert.Equal(Context.Entry(entity).Property("Modifier").CurrentValue, int.Parse(CurrentUser.Id!));
        }
    }
}
