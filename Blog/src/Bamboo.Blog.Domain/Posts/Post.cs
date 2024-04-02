﻿using Bamboo.Posts.DomainEvents.V1;
using SharedKernel.Domain;
using SharedKernel.Domain.Audit;

namespace Bamboo.Posts
{
    /// <summary>
    /// 文章
    /// </summary>
    public sealed partial class Post : AggregateRoot<Guid>, ICreationAudit, IModificationAudit, ISoftDeleteAudit
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
        public Post(Guid id, string title, string content, Guid authorId, DateTime publicationTime) => 
            RaiseEvent(new PostCreatedDomainEvent(id, title, content, authorId, publicationTime));

        /// <summary>
        /// 删除文章
        /// </summary>
        public void Remove() => RaiseEvent(new PostDeletedDomainEvent(Id));
    }

    partial class Post : IDomainEventApplier<PostCreatedDomainEvent>
    {
        void IDomainEventApplier<PostCreatedDomainEvent>.Apply(PostCreatedDomainEvent domainEvent) =>
            (Id, Title, Content, AuthorId, PublicationTime) = domainEvent;
    }
}
