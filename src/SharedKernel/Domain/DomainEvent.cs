using MediatR;
using System.Runtime.CompilerServices;

namespace SharedKernel.Domain;

/// <summary>
/// 领域事件
/// </summary>
public record class DomainEvent : INotification
{
    /// <summary>
    /// 序号
    /// </summary>
    private uint _seq;
}