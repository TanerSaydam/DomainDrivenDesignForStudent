namespace DomainDrivenDesign.Domain.Abstractions;

public abstract class Entity : IEquatable<Entity>
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
    public Guid? DeletedUserId { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity);
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other is Entity entity && entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }
}