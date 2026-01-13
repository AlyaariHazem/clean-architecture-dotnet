using System.Linq.Expressions;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Generic repository interface following Clean Architecture principles.
    /// Note: SaveChanges is handled by Unit of Work, not the repository.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        // Query methods - return IQueryable for composition
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        // Single entity operations
        Task<TEntity?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull;
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        // Collection operations
        Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default);
        Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        
        // Specification pattern support
        Task<List<TEntity>> ToListAsync(Specifications.ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(Specifications.ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Specifications.ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<Specifications.PagedResult<TEntity>> ToPagedListAsync(Specifications.ISpecification<TEntity> specification, Specifications.PaginationParams pagination, CancellationToken cancellationToken = default);

        // Write operations (tracked by Unit of Work)
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull;
    }
}
