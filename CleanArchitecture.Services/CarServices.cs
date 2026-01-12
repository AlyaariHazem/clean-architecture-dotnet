using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastracture.Interfaces;

namespace CleanArchitecture.Services
{
    public class CarServices : ICarServices
    {
        private readonly IRepository<Cars> _repository;
        public CarServices(IRepository<Cars> repository)
        {
            _repository = repository;
        }

     public async ValueTask<Cars> Create(Cars cars)
        {
           return await _repository.AddAsync(cars);
        }
    }
}
