using System.Runtime.CompilerServices;

namespace SharedKernel.Domain.Accessors;

/// <summary>
/// 领域事件访问器
/// </summary>
internal class DomainEventUnsafeAccessor
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_seq")]
    public static extern ref uint Seq(DomainEvent domainEvent);
}
