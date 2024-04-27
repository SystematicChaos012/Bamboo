using MediatR;

namespace SharedKernel.Domain.Notifications
{
    /// <summary>
    /// 聚合根更新通知 (注: 用于领域内部通知，便于获取更新信息以方便管理，此通知非领域事件)
    /// </summary>
    public sealed record AggregateRootModifiedNotification<T, TKey>(T AggregateRoot, TKey Id) where T : AggregateRoot<TKey>, INotification;
}
