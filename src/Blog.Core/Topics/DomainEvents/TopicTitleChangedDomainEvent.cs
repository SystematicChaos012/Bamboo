﻿using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题标题修改
/// </summary>
public sealed record class TopicTitleChangedDomainEvent(TopicId Id, string OldTitle, string NewTitle) : DomainEvent;