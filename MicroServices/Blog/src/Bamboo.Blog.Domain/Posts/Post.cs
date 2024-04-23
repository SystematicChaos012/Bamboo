using Audit;
using Bamboo.Posts.DomainEvents;
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
        /// 作者
        /// </summary>
        public Guid AuthorId { get; private set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublicationTime { get; private set; }

        /// <summary>
        /// Used by EF Core
        /// </summary>
        private Post() { }

        /// <summary>
        /// 创建 Post
        /// </summary>
        public Post(PostId id, string title, string content, Guid authorId, DateTime publicationTime) =>
            RaiseEvent(new PostCreatedDomainEvent(id, title, content, authorId, publicationTime));

        /// <summary>
        /// 更新标题
        /// </summary>
        public void UpdateTitle() => RaiseEvent(new PostTitleChangedDomainEvent(Id, Content));

        /// <summary>
        /// 删除文章
        /// </summary>
        public void Remove() => RaiseEvent(new PostDeletedDomainEvent(Id));
    }

    partial class Post 
        : IDomainEventApplier<PostCreatedDomainEvent>
        , IDomainEventApplier<PostDeletedDomainEvent>
        , IDomainEventApplier<PostTitleChangedDomainEvent>
    {
        void IDomainEventApplier<PostCreatedDomainEvent>.Apply(PostCreatedDomainEvent domainEvent) =>
            (Id, Title, Content, AuthorId, PublicationTime) = domainEvent;

        void IDomainEventApplier<PostTitleChangedDomainEvent>.Apply(PostTitleChangedDomainEvent domainEvent) =>
            (_, Title) = domainEvent;

        void IDomainEventApplier<PostDeletedDomainEvent>.Apply(PostDeletedDomainEvent domainEvent) { }
    }
}
