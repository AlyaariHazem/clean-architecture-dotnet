using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Services
{
    public class CarServices : ICarServices
    {
        private readonly IRepository<Cars> _repository;

        public CarServices(IRepository<Cars> repository)
        {
            _repository = repository;
        }

        public async ValueTask<Cars> CreateAsync(Cars car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            var createdCar = await _repository.AddAsync(car);
            await _repository.SaveChangesAsync();
            return createdCar;
        }

        public async ValueTask<Cars?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async ValueTask<IEnumerable<Cars>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async ValueTask<Cars> UpdateAsync(Cars car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            if (car.Id <= 0)
                throw new ArgumentException("Car Id must be greater than zero", nameof(car));

            var existingCar = await _repository.GetByIdAsync(car.Id);
            if (existingCar == null)
                throw new KeyNotFoundException($"Car with Id {car.Id} not found");

            existingCar.CarName = car.CarName;
            existingCar.CarType = car.CarType;
            existingCar.Manufacturer = car.Manufacturer;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.UpdatedAt = DateTime.UtcNow;

            _repository.Update(existingCar);
            await _repository.SaveChangesAsync();
            return existingCar;
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero", nameof(id));

            var car = await _repository.GetByIdAsync(id);
            if (car == null)
                return false;

            _repository.Delete(car);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
