﻿using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题创建
/// </summary>
public sealed record class TopicCreatedDomainEvent(TopicId TopicId, string Title, string Content) : DomainEvent;
