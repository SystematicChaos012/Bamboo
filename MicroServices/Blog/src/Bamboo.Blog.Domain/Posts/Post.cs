using Audit;
using Bamboo.Posts.DomainEvents;
using Bamboo.Posts.Entities;
using Bamboo.Posts.Enums;
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
        public DateTime? PostedTime { get; private set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PostStatus Status { get; private set; } = null!;

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
        public Post(PostId id, string title, string content)
        {
            RaiseEvent(new PostCreatedDomainEvent(id, title, content));
        }

        /// <summary>
        /// 添加作者
        /// </summary>
        /// <param name="authorId">作者 Id</param>
        /// <param name="name">作者名称</param>
        /// <exception cref="PostAuthorAlreadyExistsException">作者已存在异常</exception>
        public void AddAuthor(PostAuthorId authorId, string name)
        {
            var author = _authors.Find(x => x.Id == authorId);
            if (author is not null)
            {
                throw new PostAuthorAlreadyExistsException();
            }

            RaiseEvent(new PostAuthorAddedDomainEvent(authorId, Id, name));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <exception cref="PostAlreadyPublishedException">文章已发布异常</exception>
        /// <exception cref="PostAuthorNotFoundException">文章无作者异常</exception>
        public void ToPublish()
        {
            if (Status == PostStatus.Published)
            {
                throw new PostAlreadyPublishedException();
            }

            if (_authors.Count == 0)
            {
                throw new PostAuthorNotFoundException();
            }

            RaiseEvent(new PostPublishedDomainEvent(Id));
        }
    }

    partial class Post : IDomainEventApplier<PostCreatedDomainEvent>, IDomainEventApplier<PostAuthorAddedDomainEvent>, IDomainEventApplier<PostPublishedDomainEvent>
    {
        void IDomainEventApplier<PostCreatedDomainEvent>.Apply(PostCreatedDomainEvent domainEvent)
        {
            (Id, Title, Content) = domainEvent;
            Status = PostStatus.Draft;
        }

        void IDomainEventApplier<PostAuthorAddedDomainEvent>.Apply(PostAuthorAddedDomainEvent domainEvent)
        {
            _authors.Add(new PostAuthor(domainEvent.Id, domainEvent.PostId, domainEvent.Name));
        }

        void IDomainEventApplier<PostPublishedDomainEvent>.Apply(PostPublishedDomainEvent domainEvent)
        {
            Status = PostStatus.Published;
            PostedTime = DateTime.UtcNow;
        }
    }
}
