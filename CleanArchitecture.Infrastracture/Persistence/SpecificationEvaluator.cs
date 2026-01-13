using System.Linq.Expressions;
using CleanArchitecture.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastracture.Persistence
{
    /// <summary>
    /// Evaluates specifications and applies them to IQueryable
    /// </summary>
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification) where T : class
        {
            var query = inputQuery;

            // Disable tracking if specified
            if (!specification.IsTrackingEnabled)
            {
                query = query.AsNoTracking();
            }

            // Apply criteria (filtering)
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply includes (eager loading)
            query = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            // Apply string-based includes (for collection includes)
            query = specification.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            // Apply ordering
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply then by (secondary sorting)
            if (specification.ThenBy != null)
            {
                if (query is IOrderedQueryable<T> orderedQuery)
                {
                    query = orderedQuery.ThenBy(specification.ThenBy);
                }
            }
            else if (specification.ThenByDescending != null)
            {
                if (query is IOrderedQueryable<T> orderedQuery)
                {
                    query = orderedQuery.ThenByDescending(specification.ThenByDescending);
                }
            }

            // Apply pagination
            if (specification.Skip.HasValue)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take.HasValue)
            {
                query = query.Take(specification.Take.Value);
            }

            return query;
        }
    }
}
