using Blog.Core.Topics.DomainEvents;
using Blog.Core.Topics.Entities;
using SharedKernel.Domain;

namespace Blog.Core.Topics;

partial class Topic
    : IDomainEventApplier<TopicCreatedDomainEvent>
        , IDomainEventApplier<TopicAuthorAddedDomainEvent>
{
    void IDomainEventApplier<TopicCreatedDomainEvent>.Apply(TopicCreatedDomainEvent domainEvent) =>
        (Id, Title, Content) = domainEvent;

    void IDomainEventApplier<TopicAuthorAddedDomainEvent>.Apply(TopicAuthorAddedDomainEvent domainEvent)
    {
        var author = new TopicAuthor(
            domainEvent.TopicId,
            domainEvent.TopicAuthorId,
            domainEvent.IdentityUserId,
            domainEvent.IdentityUserName);
        
        Authors.Add(author);
    }
}