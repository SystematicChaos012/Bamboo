using System.Runtime.CompilerServices;
using SharedKernel.Ddd;
using SharedKernel.Ddd.Events;

namespace AnalyzerDebugger.Domain;

public partial class MyAggregateRoot : IAggregateRoot<Guid>
{
    public Guid Id { get; private set; } = default!;
    
    public MyAggregateRoot()
    {
        Raise(new DomainEvent());
    }
}