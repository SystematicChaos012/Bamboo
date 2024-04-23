using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;
using System.Security.Claims;

namespace Bamboo.EntityFrameworkCore
{
    public class BlogDbContext_Tests : IDisposable
    {
        FakeCurrentUser CurrentUser { get; }
        SqliteConnection SqliteConnection { get; }
        ServiceProvider ServiceProvider { get; }
        BlogDbContext BlogDbContext { get; }

        public BlogDbContext_Tests()
        {
            CurrentUser = new FakeCurrentUser();

            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            ServiceProvider = new ServiceCollection()
                .AddScoped<ICurrentUser>(_ => CurrentUser)
                .BuildServiceProvider();

            BlogDbContext = new BlogDbContext(
                ServiceProvider, 
                new DbContextOptionsBuilder<BlogDbContext>()
                    .UseSqlite(SqliteConnection)
                    .Options);
        }

        // TODO: 添加 BlogDbContext 相关单元测试

        public void Dispose()
        {
            BlogDbContext.Dispose();
            ServiceProvider.Dispose();
            SqliteConnection.Dispose();
            GC.SuppressFinalize(this);
        }

        private class FakeCurrentUser : ICurrentUser
        {
            public string Id => Guid.NewGuid().ToString();

            public string Name => "Fake Name";

            public Claim? FindClaim(string claimType)
            {
                return null;
            }
        }
    }
}
