using Audit.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;
using System.Security.Claims;

namespace Audit
{
    public class UnitTestWithoutAuthBase : IDisposable
    {
        protected ICurrentUser CurrentUser { get; }
        protected ServiceProvider ServiceProvider { get; }
        protected SqliteConnection SqliteConnection { get; }
        protected AuditDbContext Context { get; }

        public UnitTestWithoutAuthBase()
        {
            CurrentUser = new FakeCurrentUser();
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            ServiceProvider = new ServiceCollection()
                .AddScoped(_ => CurrentUser)
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

        private class FakeCurrentUser : ICurrentUser
        {
            public string? Id => null;

            public string? Name => null;

            public bool IsAuthenticated => false;

            public Claim? FindClaim(string claimType) => null;
        }
    }
}
