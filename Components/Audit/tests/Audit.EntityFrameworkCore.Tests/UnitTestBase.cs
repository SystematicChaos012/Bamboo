using Audit.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Security;
using System.Security.Claims;

namespace Audit
{
    public class UnitTestBase : IDisposable
    {
        protected IIdentityContext IdentityContext { get; }
        protected ServiceProvider ServiceProvider { get; }
        protected AuditOptions Options { get; private set; }
        protected SqliteConnection SqliteConnection { get; }
        protected AuditDbContext Context { get; }

        public UnitTestBase()
        {
            IdentityContext = new FakeCurrentUser();
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            ServiceProvider = new ServiceCollection()
                .AddScoped(_ => IdentityContext)
                .Configure<AuditOptions>((options) => { })
                .BuildServiceProvider();

            Options = ServiceProvider.GetRequiredService<IOptions<AuditOptions>>().Value;

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
            public bool IsAuthenticated => true;

            public IEnumerable<string> FindClaims(string type)
            {
                throw new NotImplementedException();
            }

            public string? FindClaim(string type)
            {
                return type switch
                {
                    "sub" => "1",
                    _ => null,
                };
            }
        }
    }
}
