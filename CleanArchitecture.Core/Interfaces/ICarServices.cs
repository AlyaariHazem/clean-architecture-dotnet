using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Specifications;

namespace CleanArchitecture.Core.Interfaces
{
    public interface ICarServices
    {
        Task<Cars> CreateAsync(Cars car);
        Task<Cars?> GetByIdAsync(int id);
        Task<IEnumerable<Cars>> GetAllAsync();
        Task<Cars> UpdateAsync(Cars car);
        Task<bool> DeleteAsync(int id);
        
        // Additional methods demonstrating Specification pattern
        Task<PagedResult<Cars>> GetPagedAsync(PaginationParams pagination, string? manufacturer = null, int? minYear = null);
        Task<IEnumerable<Cars>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
