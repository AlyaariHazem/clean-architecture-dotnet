namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Unit of Work pattern interface for managing transactions and repositories.
    /// Ensures all repositories share the same DbContext and transaction boundary.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets a repository for the specified entity type
        /// </summary>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in this unit of work to the database
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves all changes made in this unit of work to the database (synchronous)
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Begins a database transaction
        /// </summary>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the current transaction
        /// </summary>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back the current transaction
        /// </summary>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
