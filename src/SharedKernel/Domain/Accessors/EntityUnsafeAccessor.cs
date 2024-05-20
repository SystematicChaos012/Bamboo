using SharedKernel.Sequences;
using System.Runtime.CompilerServices;

namespace SharedKernel.Domain.Accessors;

/// <summary>
/// 实体访问器
/// </summary>
internal class EntityUnsafeAccessor
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_sequentialValue")]
    public static extern ref SequentialValue SequentialValue(Entity entity);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_domainEvents")]
    public static extern ref List<DomainEvent> DomainEvents(Entity entity);
}