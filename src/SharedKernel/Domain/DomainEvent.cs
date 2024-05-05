using MediatR;

namespace SharedKernel.Domain;

/// <summary>
/// 领域事件
/// </summary>
public record class DomainEvent : INotification;