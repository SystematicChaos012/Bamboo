﻿using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题内容修改
/// </summary>
public sealed record class TopicContentChangedDomainEvent(TopicId Id, string OldContent, string NewContent) : DomainEvent;