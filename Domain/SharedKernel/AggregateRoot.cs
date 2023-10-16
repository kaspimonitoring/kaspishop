namespace Domain.SharedKernel;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot() { } // For EF
    protected AggregateRoot(TId id) : base(id)
    {
    }
}