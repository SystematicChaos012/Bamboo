using Blog.Core.Topics.DomainEvents;
using SharedKernel.Domain;

namespace Blog.Core.Topics;

#pragma warning disable

partial class Topic
    : IDomainEventApplier<TopicCreatedDomainEvent>
        , IDomainEventApplier<TopicAuthorAddedDomainEvent>
        , IDomainEventApplier<TopicAuthorRemovedDomainEvent>
        , IDomainEventApplier<TopicTitleModifiedDomainEvent>
        , IDomainEventApplier<TopicContentModifiedDomainEvent>
{
    void IDomainEventApplier<TopicCreatedDomainEvent>.Apply(TopicCreatedDomainEvent domainEvent) =>
        (Id, Title, Content) = domainEvent;

    void IDomainEventApplier<TopicAuthorAddedDomainEvent>.Apply(TopicAuthorAddedDomainEvent domainEvent) =>
        Authors.Add(domainEvent.CreateTopicAuthor());

    void IDomainEventApplier<TopicAuthorRemovedDomainEvent>.Apply(TopicAuthorRemovedDomainEvent domainEvent) => 
        Authors.Remove(Authors.FirstOrDefault(x => x.Id == domainEvent.TopicAuthorId));

    void IDomainEventApplier<TopicTitleModifiedDomainEvent>.Apply(TopicTitleModifiedDomainEvent domainEvent) =>
        Title = domainEvent.NewTitle;

    void IDomainEventApplier<TopicContentModifiedDomainEvent>.Apply(TopicContentModifiedDomainEvent domainEvent) =>
        Content = domainEvent.NewContent;
}

#pragma warning restore