namespace CleanArchitecture.Core.Entities
{
    /// <summary>
    /// Base entity interface for entities with a primary key
    /// </summary>
    public interface IEntity<TKey> where TKey : notnull
    {
        TKey Id { get; set; }
    }

    /// <summary>
    /// Base entity with common audit fields
    /// </summary>
    public abstract class BaseEntity<TKey> : IEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Convenience base entity with int primary key
    /// </summary>
    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
