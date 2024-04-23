using Bamboo.Posts.Exceptions;
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

            var author = Assert.Single(post.Authors);
            Assert.Equal(author.Id, authorId);
            Assert.Equal(author.Name, authorName);
            Assert.Equal(author.PostId, post.Id);
        }

        [Fact]
        public void Post_Add_Repeat_Author()
        {
            var post = new Post(new PostId(Guid.NewGuid()), "Title", "Content", DateTime.UtcNow);
            var authorId = new PostAuthorId(Guid.NewGuid());
            var authorName = "Author";

            post.AddAuthor(authorId, authorName);

            Assert.Throws<PostAuthorAlreadyExistsException>(() => post.AddAuthor(authorId, authorName));
        }
    }
}
