﻿using Blog.Core.Topics.Entities;
using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics.DomainEvents;

/// <summary>
/// 主题作者添加领域事件
/// </summary>
/// <param name="TopicId">主题 Id</param>
/// <param name="TopicAuthorId">主题作者 Id</param>
/// <param name="IdentityUserId">用户 Id</param>
/// <param name="IdentityUserName">用户名称</param>
public sealed record class TopicAuthorAddedDomainEvent(
    TopicId TopicId,
    TopicAuthorId TopicAuthorId,
    IdentityUserId IdentityUserId,
    string IdentityUserName) : DomainEvent
{
    /// <summary>
    /// 创建作者
    /// </summary>
    public TopicAuthor CreateTopicAuthor() => new(TopicId, TopicAuthorId, IdentityUserId, IdentityUserName);
}