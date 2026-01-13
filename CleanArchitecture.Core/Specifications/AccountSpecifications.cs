using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Specifications
{
    /// <summary>
    /// Specification to get an account by its ID.
    /// </summary>
    public class AccountByIdSpecification : BaseSpecification<Accounts>
    {
        public AccountByIdSpecification(Guid id)
        {
            Criteria = account => account.AccountID == id;
            AddInclude(account => account.TypeAccount);
            EnableTracking(false);
        }
    }

    /// <summary>
    /// Specification to get all accounts with optional filtering.
    /// </summary>
    public class AllAccountsSpecification : BaseSpecification<Accounts>
    {
        public AllAccountsSpecification(string? accountName = null, bool? state = null, Guid? typeAccountID = null)
        {
            System.Linq.Expressions.Expression<Func<Accounts, bool>>? combinedCriteria = null;

            if (!string.IsNullOrWhiteSpace(accountName))
            {
                combinedCriteria = account => account.AccountName != null && account.AccountName.Contains(accountName);
            }

            if (state.HasValue)
            {
                var stateCriteria = (System.Linq.Expressions.Expression<Func<Accounts, bool>>)(account => account.State == state.Value);
                combinedCriteria = combinedCriteria == null
                    ? stateCriteria
                    : CombineExpressions(combinedCriteria, stateCriteria);
            }

            if (typeAccountID.HasValue)
            {
                var typeCriteria = (System.Linq.Expressions.Expression<Func<Accounts, bool>>)(account => account.TypeAccountID == typeAccountID.Value);
                combinedCriteria = combinedCriteria == null
                    ? typeCriteria
                    : CombineExpressions(combinedCriteria, typeCriteria);
            }

            Criteria = combinedCriteria;
            AddInclude(account => account.TypeAccount);
            ApplyOrderBy(account => account.AccountName ?? string.Empty);
        }

        private static System.Linq.Expressions.Expression<Func<Accounts, bool>> CombineExpressions(
            System.Linq.Expressions.Expression<Func<Accounts, bool>> left,
            System.Linq.Expressions.Expression<Func<Accounts, bool>> right)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(Accounts));
            var leftBody = ReplaceParameter(left.Body, left.Parameters[0], parameter);
            var rightBody = ReplaceParameter(right.Body, right.Parameters[0], parameter);
            var combined = System.Linq.Expressions.Expression.AndAlso(leftBody, rightBody);
            return System.Linq.Expressions.Expression.Lambda<Func<Accounts, bool>>(combined, parameter);
        }

        private static System.Linq.Expressions.Expression ReplaceParameter(
            System.Linq.Expressions.Expression expression,
            System.Linq.Expressions.ParameterExpression oldParam,
            System.Linq.Expressions.ParameterExpression newParam)
        {
            return new ParameterReplacer(oldParam, newParam).Visit(expression);
        }

        private class ParameterReplacer : System.Linq.Expressions.ExpressionVisitor
        {
            private readonly System.Linq.Expressions.ParameterExpression _oldParam;
            private readonly System.Linq.Expressions.ParameterExpression _newParam;

            public ParameterReplacer(System.Linq.Expressions.ParameterExpression oldParam, System.Linq.Expressions.ParameterExpression newParam)
            {
                _oldParam = oldParam;
                _newParam = newParam;
            }

            protected override System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node)
            {
                return node == _oldParam ? _newParam : base.VisitParameter(node);
            }
        }
    }

    /// <summary>
    /// Specification for paged account results with optional filtering.
    /// </summary>
    public class PagedAccountsSpecification : BaseSpecification<Accounts>
    {
        public PagedAccountsSpecification(PaginationParams pagination, string? accountName = null, bool? state = null, Guid? typeAccountID = null)
        {
            System.Linq.Expressions.Expression<Func<Accounts, bool>>? combinedCriteria = null;

            if (!string.IsNullOrWhiteSpace(accountName))
            {
                combinedCriteria = account => account.AccountName != null && account.AccountName.Contains(accountName);
            }

            if (state.HasValue)
            {
                var stateCriteria = (System.Linq.Expressions.Expression<Func<Accounts, bool>>)(account => account.State == state.Value);
                combinedCriteria = combinedCriteria == null
                    ? stateCriteria
                    : CombineExpressions(combinedCriteria, stateCriteria);
            }

            if (typeAccountID.HasValue)
            {
                var typeCriteria = (System.Linq.Expressions.Expression<Func<Accounts, bool>>)(account => account.TypeAccountID == typeAccountID.Value);
                combinedCriteria = combinedCriteria == null
                    ? typeCriteria
                    : CombineExpressions(combinedCriteria, typeCriteria);
            }

            Criteria = combinedCriteria;
            AddInclude(account => account.TypeAccount);
            ApplyOrderBy(account => account.AccountName ?? string.Empty);
            ApplyPaging(pagination.Skip, pagination.Take);
        }

        private static System.Linq.Expressions.Expression<Func<Accounts, bool>> CombineExpressions(
            System.Linq.Expressions.Expression<Func<Accounts, bool>> left,
            System.Linq.Expressions.Expression<Func<Accounts, bool>> right)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(Accounts));
            var leftBody = ReplaceParameter(left.Body, left.Parameters[0], parameter);
            var rightBody = ReplaceParameter(right.Body, right.Parameters[0], parameter);
            var combined = System.Linq.Expressions.Expression.AndAlso(leftBody, rightBody);
            return System.Linq.Expressions.Expression.Lambda<Func<Accounts, bool>>(combined, parameter);
        }

        private static System.Linq.Expressions.Expression ReplaceParameter(
            System.Linq.Expressions.Expression expression,
            System.Linq.Expressions.ParameterExpression oldParam,
            System.Linq.Expressions.ParameterExpression newParam)
        {
            return new ParameterReplacer(oldParam, newParam).Visit(expression);
        }

        private class ParameterReplacer : System.Linq.Expressions.ExpressionVisitor
        {
            private readonly System.Linq.Expressions.ParameterExpression _oldParam;
            private readonly System.Linq.Expressions.ParameterExpression _newParam;

            public ParameterReplacer(System.Linq.Expressions.ParameterExpression oldParam, System.Linq.Expressions.ParameterExpression newParam)
            {
                _oldParam = oldParam;
                _newParam = newParam;
            }

            protected override System.Linq.Expressions.Expression VisitParameter(System.Linq.Expressions.ParameterExpression node)
            {
                return node == _oldParam ? _newParam : base.VisitParameter(node);
            }
        }
    }
}
