﻿using Bamboo.Posts.Entities;
using Bamboo.Posts.Enums;
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

            var post = new Post(postId, title, content);

            Assert.Equal(post.Id, postId);
            Assert.Equal(post.Title, title);
            Assert.Equal(post.Content, content);
            Assert.Equal(post.Status, PostStatus.Draft);
            Assert.Null(post.PostedTime);
        }

        [Fact]
        public void Post_Add_Author()
        {
            var post = CreatePost();
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
            var post = CreatePostWithOneAuthor(out var author);

            Assert.Throws<PostAuthorAlreadyExistsException>(() => post.AddAuthor(author.Id, author.Name));
        }

        [Fact]
        public void Post_To_Publish_Without_Author()
        {
            var post = CreatePost();

            Assert.Throws<PostAuthorNotFoundException>(post.ToPublish);
        }

        [Fact]
        public void Post_To_Publish_With_Author()
        {
            var post = CreatePostWithOneAuthor(out _);

            post.ToPublish();

            Assert.Equal(PostStatus.Published, post.Status);
            Assert.NotNull(post.PostedTime);
        }

        [Fact]
        public void Post_To_Publish_When_Published()
        {
            var post = CreatePostWithOneAuthor(out _);

            post.ToPublish();

            Assert.Throws<PostAlreadyPublishedException>(post.ToPublish);
        }

        private static Post CreatePost()
        {
            var postId = new PostId(Guid.NewGuid());
            var title = "Title";
            var content = "Content";

            return new Post(postId, title, content);
        }

        private static Post CreatePostWithOneAuthor(out PostAuthor author)
        {
            var postId = new PostId(Guid.NewGuid());
            var title = "Title";
            var content = "Content";

            var post = new Post(postId, title, content);

            var authorId = new PostAuthorId(Guid.NewGuid());
            var authorName = "Author";

            post.AddAuthor(authorId, authorName);

            author = post.Authors.Single();

            return post;
        }
    }
}