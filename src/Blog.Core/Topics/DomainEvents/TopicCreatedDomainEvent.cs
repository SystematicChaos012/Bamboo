using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题创建领域事件
/// </summary>
/// <param name="TopicId">主键</param>
/// <param name="Title">标题</param>
/// <param name="Content">内容</param>
public sealed record class TopicCreatedDomainEvent(TopicId TopicId, string Title, string Content) : DomainEvent;
