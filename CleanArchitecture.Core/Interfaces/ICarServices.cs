using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface ICarServices
    {
        ValueTask<Cars> CreateAsync(Cars car);
        ValueTask<Cars?> GetByIdAsync(int id);
        ValueTask<IEnumerable<Cars>> GetAllAsync();
        ValueTask<Cars> UpdateAsync(Cars car);
        ValueTask<bool> DeleteAsync(int id);
    }
}
