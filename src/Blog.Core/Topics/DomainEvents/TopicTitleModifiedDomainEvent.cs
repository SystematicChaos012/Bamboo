using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 修改标题
/// </summary>
public sealed record class TopicTitleModifiedDomainEvent(TopicId Id, string OldTitle, string NewTitle) : DomainEvent;