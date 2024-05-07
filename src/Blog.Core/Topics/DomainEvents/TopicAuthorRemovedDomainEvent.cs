using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题作者已删除领域事件
/// </summary>
public sealed record class TopicAuthorRemovedDomainEvent(TopicId Id, TopicAuthorId TopicAuthorId) : DomainEvent;