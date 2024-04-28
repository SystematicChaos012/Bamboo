using Audit.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Security;
using System.Security.Claims;

namespace Audit
{
    public class UnitTestWithoutAuthBase : IDisposable
    {
        protected IIdentityContext IdentityContext { get; }
        protected ServiceProvider ServiceProvider { get; }
        protected SqliteConnection SqliteConnection { get; }
        protected AuditDbContext Context { get; }

        public UnitTestWithoutAuthBase()
        {
            IdentityContext = new FakeCurrentUser();
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            ServiceProvider = new ServiceCollection()
                .AddScoped(_ => IdentityContext)
                .BuildServiceProvider();

            Context = new AuditDbContext(
                ServiceProvider,
                new DbContextOptionsBuilder<AuditDbContext>().UseSqlite(SqliteConnection).Options);

            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
            SqliteConnection.Close();
            SqliteConnection.Dispose();
            ServiceProvider.Dispose();
            GC.SuppressFinalize(this);
        }

        private class FakeCurrentUser : IIdentityContext
        {
            public bool IsAuthenticated => false;

            public IEnumerable<string> FindClaims(string type)
            {
                throw new NotImplementedException();
            }

            public string? FindClaim(string type) => null;
        }
    }
}
