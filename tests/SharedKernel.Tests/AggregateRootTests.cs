using System.Runtime.CompilerServices;
using SharedKernel.Domain;

// ReSharper disable ReturnTypeCanBeEnumerable.Local

namespace SharedKernel.Tests;

public class AggregateRootTests
{
    [Fact]
    public void AggregateRoot_Create()
    {
        var aggregateRoot = new UnitTestAggregateRoot();
        
        Assert.Single(UnitTestAggregateRootUnsafeAccessor.GetDomainEvents(aggregateRoot));
    }

    private record class UnitTestCreatedDomainEvent : DomainEvent;

    private sealed class UnitTestAggregateRoot : AggregateRoot
    {
        public UnitTestAggregateRoot() =>
            Raise(new UnitTestCreatedDomainEvent());
    }

    private static class UnitTestAggregateRootUnsafeAccessor
    {
        [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_domainEvents")]
        public static extern ref List<DomainEvent> GetDomainEvents(AggregateRoot aggregateRoot);
    }
}