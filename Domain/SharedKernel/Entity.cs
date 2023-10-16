using MediatR;

namespace Domain.SharedKernel;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; }

    protected Entity() { } // For EF
    protected Entity(TId id)
    {
        if (!IsValid(id))
        {
            throw new ArgumentException("Unsupported Id format");
        }
        Id = id;
    }

    public bool Equals(Entity<TId>? other)
    {
        return Id.GetHashCode() == other.Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity<TId>);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    private bool IsValid(TId id)
    {
        return id is int or long or Guid;
    }

    private List<INotification> _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents;
    protected void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }
    protected void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}