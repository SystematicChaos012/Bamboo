using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题作者创建
/// </summary>
public sealed record class TopicAuthorCreatedDomainEvent(TopicAuthorId Id, TopicId TopicId, AuthorId AuthorId, string AuthorName) : DomainEvent;