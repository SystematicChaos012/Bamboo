using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts.Entities
{
    /// <summary>
    /// 作者
    /// </summary>
    public sealed class PostAuthor(PostAuthorId id, PostId postId, string name) : Entity<PostAuthorId>(id)
    {
        /// <summary>
        /// 文章 Id
        /// </summary>
        public PostId PostId { get; private set; } = postId;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// 更改名称
        /// </summary>
        public void ChangeName(string newName) => Name = newName;
    }
}
