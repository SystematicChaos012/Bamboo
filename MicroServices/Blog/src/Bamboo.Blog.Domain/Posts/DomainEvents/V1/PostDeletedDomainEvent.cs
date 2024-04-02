﻿using SharedKernel.Domain;

namespace Bamboo.Posts.DomainEvents.V1
{
    /// <summary>
    /// Post 删除领域事件
    /// </summary>
    public sealed record class PostDeletedDomainEvent(Guid Id) : DomainEvent;
}
