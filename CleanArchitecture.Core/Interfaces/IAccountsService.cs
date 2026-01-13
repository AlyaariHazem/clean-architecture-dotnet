using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Specifications;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Interface for account-related services, defining CRUD and query operations.
    /// </summary>
    public interface IAccountsService
    {
        Task<Accounts> CreateAsync(Accounts account);
        Task<Accounts?> GetByIdAsync(Guid id);
        Task<IEnumerable<Accounts>> GetAllAsync(string? accountName = null, bool? state = null, Guid? typeAccountID = null);
        Task<Accounts> UpdateAsync(Accounts account);
        Task<bool> DeleteAsync(Guid id);
        Task<PagedResult<Accounts>> GetPagedAsync(PaginationParams pagination, string? accountName = null, bool? state = null, Guid? typeAccountID = null);
    }
}
