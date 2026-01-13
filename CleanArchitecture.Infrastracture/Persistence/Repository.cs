using System.Linq.Expressions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Specifications;
using CleanArchitecture.Infrastracture.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastracture.Persistence
{
    /// <summary>
    /// Generic repository implementation using Entity Framework Core.
    /// Follows Clean Architecture by keeping EF Core in the Infrastructure layer.
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual async Task<TEntity?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull
        {
            return await DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return predicate == null
                ? await DbSet.CountAsync(cancellationToken)
                : await DbSet.CountAsync(predicate, cancellationToken);
        }

        public virtual async Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> ToListAsync(Core.Specifications.ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(specification);
            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Core.Specifications.ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(specification);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Core.Specifications.ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            // Create a count query without pagination
            var query = ApplySpecification(specification);
            return await query.CountAsync(cancellationToken);
        }

        public virtual async Task<Core.Specifications.PagedResult<TEntity>> ToPagedListAsync(
            Core.Specifications.ISpecification<TEntity> specification,
            Core.Specifications.PaginationParams pagination,
            CancellationToken cancellationToken = default)
        {
            // Apply pagination
            var pagedSpec = new PagedSpecification<TEntity>(specification, pagination);
            var items = await ToListAsync(pagedSpec, cancellationToken);
            var totalCount = await CountAsync(specification, cancellationToken);

            return new Core.Specifications.PagedResult<TEntity>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        private class PagedSpecification<T> : Core.Specifications.BaseSpecification<T>
        {
            public PagedSpecification(Core.Specifications.ISpecification<T> source, Core.Specifications.PaginationParams pagination)
            {
                Criteria = source.Criteria;
                Includes.AddRange(source.Includes);
                IncludeStrings.AddRange(source.IncludeStrings);
                if (source.OrderBy != null) ApplyOrderBy(source.OrderBy);
                if (source.OrderByDescending != null) ApplyOrderByDescending(source.OrderByDescending);
                if (source.ThenBy != null) ApplyThenBy(source.ThenBy);
                if (source.ThenByDescending != null) ApplyThenByDescending(source.ThenByDescending);
                EnableTracking(source.IsTrackingEnabled);
                ApplyPaging(pagination.Skip, pagination.Take);
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await DbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            DbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            DbSet.RemoveRange(entities);
        }

        public virtual async Task DeleteAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Applies specification to queryable (used by UnitOfWork)
        /// </summary>
        public virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator.GetQuery(DbSet.AsQueryable(), specification);
        }
    }
}
