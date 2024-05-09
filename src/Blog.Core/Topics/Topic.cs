﻿using Blog.Core.Topics.DomainEvents;
using Blog.Core.Topics.DomainExceptions;
using Blog.Core.Topics.Entities;
using Blog.Core.Topics.ValueObjects;
using SharedKernel.Domain;

namespace Blog.Core.Topics;

/// <summary>
/// 主题
/// </summary>
public sealed partial class Topic : AggregateRoot
{
    /// <summary>
    /// 主键
    /// </summary>
    public TopicId Id { get; private set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; private set; } = null!;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; private set; } = null!;

    /// <summary>
    /// 主题作者
    /// </summary>
    public ICollection<TopicAuthor> Authors { get; } = [];

    /// <summary>
    /// 创建主题
    /// </summary>
    /// <param name="id">主键 Id</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    public Topic(TopicId id, string title, string content) => 
        Raise(new TopicCreatedDomainEvent(id, title, content));

    /// <summary>
    /// 修改标题
    /// </summary>
    /// <param name="title">新标题</param>
    public void ModifyTitle(string title) => 
        Raise(new TopicTitleModifiedDomainEvent(Id, Title, title));

    /// <summary>
    /// 修改内容
    /// </summary>
    /// <param name="content">新内容</param>
    public void ModifyContent(string content) =>
        Raise(new TopicContentModifiedDomainEvent(Id, Content, content));

    /// <summary>
    /// 添加作者
    /// </summary>
    /// <param name="topicAuthorId">主键 Id</param>
    /// <param name="authorId">用户 Id</param>
    /// <param name="authorName">用户名称</param>
    public void AddAuthor(TopicAuthorId topicAuthorId, AuthorId authorId, string authorName)
    {
        // 作者已存在
        if (Authors.Any(x => x.AuthorId == authorId))
        {
            TopicThrowHelper.AuthorAddedTwice();
        }

        Raise(new TopicAuthorAddedDomainEvent(Id, topicAuthorId, authorId, authorName));
    }

    /// <summary>
    /// 删除作者
    /// </summary>
    /// <param name="topicAuthorId">主键 Id</param>
    public void RemoveAuthor(TopicAuthorId topicAuthorId)
    {
        // 作者未存在
        var author = Authors.FirstOrDefault(x => x.Id == topicAuthorId);
        if (author is null)
        {
            TopicThrowHelper.AuthorNotFound();
        }
        
        Raise(new TopicAuthorRemovedDomainEvent(Id, topicAuthorId));
    }
}