using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 修改标题
/// </summary>
public sealed record class TopicContentChangedDomainEvent(TopicId Id, string OldContent, string NewContent) : DomainEvent;