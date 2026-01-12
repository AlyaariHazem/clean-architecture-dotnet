using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface ICarServices
    {
        ValueTask<Cars> Create(Cars cars);
    }
}
