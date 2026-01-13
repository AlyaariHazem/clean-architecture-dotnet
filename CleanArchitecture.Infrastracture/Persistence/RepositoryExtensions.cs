using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastracture.Persistence
{
    /// <summary>
    /// Extension methods for repository to work with specifications
    /// </summary>
    public static class RepositoryExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(
            this IRepository<T> repository,
            ISpecification<T> specification,
            CancellationToken cancellationToken = default) where T : class
        {
            if (repository is Repository<T> efRepository)
            {
                var query = efRepository.ApplySpecification(specification);
                return await query.ToListAsync(cancellationToken);
            }

            throw new InvalidOperationException("Specification pattern requires EF Core repository implementation");
        }

        public static async Task<T?> FirstOrDefaultAsync<T>(
            this IRepository<T> repository,
            ISpecification<T> specification,
            CancellationToken cancellationToken = default) where T : class
        {
            if (repository is Repository<T> efRepository)
            {
                var query = efRepository.ApplySpecification(specification);
                return await query.FirstOrDefaultAsync(cancellationToken);
            }

            throw new InvalidOperationException("Specification pattern requires EF Core repository implementation");
        }

        public static async Task<int> CountAsync<T>(
            this IRepository<T> repository,
            ISpecification<T> specification,
            CancellationToken cancellationToken = default) where T : class
        {
            if (repository is Repository<T> efRepository)
            {
                // Create a count specification without pagination
                var countSpec = new CountSpecification<T>(specification);
                var query = efRepository.ApplySpecification(countSpec);
                return await query.CountAsync(cancellationToken);
            }

            throw new InvalidOperationException("Specification pattern requires EF Core repository implementation");
        }

        public static async Task<PagedResult<T>> ToPagedListAsync<T>(
            this IRepository<T> repository,
            ISpecification<T> specification,
            PaginationParams pagination,
            CancellationToken cancellationToken = default) where T : class
        {
            // Apply pagination to specification
            var pagedSpec = new PagedSpecification<T>(specification, pagination);

            var items = await repository.ToListAsync(pagedSpec, cancellationToken);
            var totalCount = await repository.CountAsync(specification, cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        // Helper classes for pagination
        private class CountSpecification<T> : BaseSpecification<T>
        {
            public CountSpecification(ISpecification<T> source)
            {
                Criteria = source.Criteria;
                Includes.AddRange(source.Includes);
                IncludeStrings.AddRange(source.IncludeStrings);
                // Don't copy pagination or ordering for count
            }
        }

        private class PagedSpecification<T> : BaseSpecification<T>
        {
            public PagedSpecification(ISpecification<T> source, PaginationParams pagination)
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
    }
}
