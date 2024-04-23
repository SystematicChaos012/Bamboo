using Bamboo.Posts.ValueObjects;

namespace Bamboo.Posts
{
    public class PostTests
    {
        [Fact]
        public void Post_Create()
        {
            var postId = new PostId(Guid.NewGuid());
            var title = "Title";
            var content = "Content";
            var postedTime = DateTime.UtcNow;

            var post = new Post(postId, title, content, postedTime);

            Assert.Equal(post.Id, postId);
            Assert.Equal(post.Title, title);
            Assert.Equal(post.Content, content);
            Assert.Equal(post.PostedTime, postedTime);
        }

        [Fact]
        public void Post_Add_Author()
        {
            var post = new Post(new PostId(Guid.NewGuid()), "Title", "Content", DateTime.UtcNow);
            var authorId = new PostAuthorId(Guid.NewGuid());
            var authorName = "Author";

            post.AddAuthor(authorId, authorName);

            Assert.Single(post.Authors);
            Assert.Equal(post.Authors.Single().Id, authorId);
            Assert.Equal(post.Authors.Single().Name, authorName);
            Assert.Equal(post.Authors.Single().PostId, post.Id);
        }
    }
}
