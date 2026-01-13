using System.Linq.Expressions;

namespace CleanArchitecture.Core.Specifications
{
    /// <summary>
    /// Specification pattern interface for encapsulating query logic
    /// </summary>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Criteria expression for filtering
        /// </summary>
        Expression<Func<T, bool>>? Criteria { get; }

        /// <summary>
        /// Include expressions for eager loading related entities
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Include strings for eager loading (for collection includes)
        /// </summary>
        List<string> IncludeStrings { get; }

        /// <summary>
        /// Order by expression
        /// </summary>
        Expression<Func<T, object>>? OrderBy { get; }

        /// <summary>
        /// Order by descending expression
        /// </summary>
        Expression<Func<T, object>>? OrderByDescending { get; }

        /// <summary>
        /// Then by expression (for secondary sorting)
        /// </summary>
        Expression<Func<T, object>>? ThenBy { get; }

        /// <summary>
        /// Then by descending expression
        /// </summary>
        Expression<Func<T, object>>? ThenByDescending { get; }

        /// <summary>
        /// Skip count for pagination
        /// </summary>
        int? Skip { get; }

        /// <summary>
        /// Take count for pagination
        /// </summary>
        int? Take { get; }

        /// <summary>
        /// Whether to enable change tracking (default: true)
        /// </summary>
        bool IsTrackingEnabled { get; }
    }

    /// <summary>
    /// Base specification implementation
    /// </summary>
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; protected set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
        public Expression<Func<T, object>>? ThenBy { get; protected set; }
        public Expression<Func<T, object>>? ThenByDescending { get; protected set; }
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public bool IsTrackingEnabled { get; protected set; } = true;

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyThenBy(Expression<Func<T, object>> thenByExpression)
        {
            ThenBy = thenByExpression;
        }

        protected void ApplyThenByDescending(Expression<Func<T, object>> thenByDescendingExpression)
        {
            ThenByDescending = thenByDescendingExpression;
        }

        protected void EnableTracking(bool enable = true)
        {
            IsTrackingEnabled = enable;
        }
    }
}
