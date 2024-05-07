using Blog.Core.Topics.DomainEvents;
using SharedKernel.Domain;

namespace Blog.Core.Topics;

#pragma warning disable

partial class Topic
    : IDomainEventApplier<TopicCreatedDomainEvent>
        , IDomainEventApplier<TopicAuthorAddedDomainEvent>
        , IDomainEventApplier<TopicAuthorRemovedDomainEvent>
{
    void IDomainEventApplier<TopicCreatedDomainEvent>.Apply(TopicCreatedDomainEvent domainEvent) =>
        (Id, Title, Content) = domainEvent;

    void IDomainEventApplier<TopicAuthorAddedDomainEvent>.Apply(TopicAuthorAddedDomainEvent domainEvent) =>
        Authors.Add(domainEvent.CreateTopicAuthor());

    void IDomainEventApplier<TopicAuthorRemovedDomainEvent>.Apply(TopicAuthorRemovedDomainEvent domainEvent) => 
        Authors.Remove(Authors.FirstOrDefault(x => x.Id == domainEvent.TopicAuthorId));
}

#pragma warning restore