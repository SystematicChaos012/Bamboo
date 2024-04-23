using Bamboo.Posts;
using Bamboo.Posts.ValueObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Profiles;
using System.Security.Claims;

namespace Bamboo.EntityFrameworkCore
{
    public class BlogDbContext_Tests : IDisposable
    {
        public class FakeCurrentUser : ICurrentUser
        {
            public string Id => "b0704e40-fb45-4c4c-b99a-3de8b3fd3040";

            public string Name => "Fake User";

            public Claim? FindClaim(string claimType)
            {
                return null;
            }
        }

        public SqliteConnection KeepAliveConnection { get; }
        public DbContextOptions<BlogDbContext> Options { get; }
        public ServiceProvider ServiceProvider { get; }
        public BlogDbContext Context { get; }

        public BlogDbContext_Tests()
        {
            KeepAliveConnection = new SqliteConnection("DataSource=:memory:");
            KeepAliveConnection.Open();

            Options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseSqlite(KeepAliveConnection)
                .Options;

            ServiceProvider = new ServiceCollection()
                .AddScoped<ICurrentUser, FakeCurrentUser>()
                .BuildServiceProvider();

            Context = new BlogDbContext(ServiceProvider, Options);
            Context.Database.EnsureCreated();
        }

        private Post CreatePost(Guid id)
        {
            var postId = new PostId(id);
            var title = "Title";
            var content = "Content";
            var authorId = Guid.NewGuid();
            var publicationTime = DateTime.UtcNow;

            return new Post(postId, title, content, authorId, publicationTime);
        }

        [Fact]
        public void SaveChangesThroughAdd_Test()
        {
            // Arrange
            var post = CreatePost(Guid.Parse("10704e40-fb45-4c4c-b99a-3de8b3fd3040"));

            // Act
            Context.Posts.Add(post);
            var result = Context.SaveChanges();

            // Assert
            Assert.True(result == 1);
        }

        [Fact]
        public void SaveChangesThroughUpdate_Test()
        {
            // Arrange
            var postAdded = CreatePost(Guid.Parse("20704e40-fb45-4c4c-b99a-3de8b3fd3040"));
            Context.Posts.Add(postAdded);
            Context.SaveChanges();

            // Act
            var post = Context.Posts.Find(postAdded.Id)!;
            post.UpdateTitle("新标题");
            var result = Context.SaveChanges();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void SaveChangesThroughDelete_Test()
        {
            // Arrange
            var postAdded = CreatePost(Guid.Parse("30704e40-fb45-4c4c-b99a-3de8b3fd3040"));
            Context.Posts.Add(postAdded);
            Context.SaveChanges();

            // Act
            var post = Context.Posts.Find(postAdded.Id)!;
            Context.Remove(post);
            var result = Context.SaveChanges();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void LogicalDeletion_Test()
        {
            // Arrange
            var postAdded = CreatePost(Guid.Parse("40704e40-fb45-4c4c-b99a-3de8b3fd3040"));
            Context.Posts.IgnoreQueryFilters().ExecuteDelete();
            Context.Posts.Add(postAdded);
            Context.SaveChanges();

            // Act
            var post = Context.Posts.Find(postAdded.Id)!;
            Context.Remove(post);
            Context.SaveChanges();
            var result = Context.Posts.ToList();
            var resultWithIgnoreQueryFilter = Context.Posts.IgnoreQueryFilters().ToList();

            // Assert
            Assert.Empty(result);
            Assert.Single(resultWithIgnoreQueryFilter);
        }

        public void Dispose()
        {
            Context.Dispose();
            KeepAliveConnection.Dispose();
            ServiceProvider.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
