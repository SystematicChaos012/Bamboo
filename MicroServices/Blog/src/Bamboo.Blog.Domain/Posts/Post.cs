using Audit;
using Bamboo.Posts.DomainEvents;
using Bamboo.Posts.Entities;
using Bamboo.Posts.Exceptions;
using Bamboo.Posts.ValueObjects;
using SharedKernel.Domain;

namespace Bamboo.Posts
{
    /// <summary>
    /// 文章
    /// </summary>
    public sealed partial class Post
        : AggregateRoot<PostId>
        , IConcurrencyStamp, ICreationTime, ICreator<Guid>, IModificationTime, IModifier<Guid>, IDeletionTime, IDeleter<Guid>, ILogicalDeletion
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; } = null!;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; } = null!;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PostedTime { get; private set; }

        /// <summary>
        /// 作者
        /// </summary>
        public ICollection<PostAuthor> Authors => _authors.AsReadOnly();
        private readonly List<PostAuthor> _authors = [];

        /// <summary>
        /// Used by EF Core
        /// </summary>
        private Post() { }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="id">文章 Id</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="postedTime">发布时间</param>
        public Post(PostId id, string title, string content, DateTime postedTime) => 
            RaiseEvent(new PostCreatedDomainEvent(id, title, content, postedTime));

        /// <summary>
        /// 添加作者
        /// </summary>
        /// <param name="authorId">作者 Id</param>
        /// <param name="name">作者名称</param>
        public void AddAuthor(PostAuthorId authorId, string name)
        {
            var author = _authors.Find(x => x.Id == authorId);
            if (author is not null)
            {
                throw new PostAuthorAlreadyExistsException();
            }

            RaiseEvent(new PostAuthorAddedDomainEvent(authorId, Id, name));
        }
    }

    partial class Post : IDomainEventApplier<PostCreatedDomainEvent>, IDomainEventApplier<PostAuthorAddedDomainEvent>
    {
        void IDomainEventApplier<PostCreatedDomainEvent>.Apply(PostCreatedDomainEvent domainEvent)
        {
            (Id, Title, Content, PostedTime) = domainEvent;
        }

        void IDomainEventApplier<PostAuthorAddedDomainEvent>.Apply(PostAuthorAddedDomainEvent domainEvent)
        {
            _authors.Add(new PostAuthor(domainEvent.Id, domainEvent.PostId, domainEvent.Name));
        }
    }
}
