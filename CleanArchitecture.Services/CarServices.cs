using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Specifications;

namespace CleanArchitecture.Services
{
    /// <summary>
    /// Car service implementation using Unit of Work pattern.
    /// All database operations are coordinated through Unit of Work for proper transaction management.
    /// </summary>
    public class CarServices : ICarServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        private IRepository<Cars> Repository => _unitOfWork.Repository<Cars>();

        public async Task<Cars> CreateAsync(Cars car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            car.CreatedAt = DateTime.UtcNow;
            var createdCar = await Repository.AddAsync(car);
            await _unitOfWork.SaveChangesAsync();
            return createdCar;
        }

        public async Task<Cars?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero", nameof(id));

            var specification = new CarByIdSpecification(id);
            return await Repository.FirstOrDefaultAsync(specification);
        }

        public async Task<IEnumerable<Cars>> GetAllAsync()
        {
            var specification = new AllCarsSpecification();
            return await Repository.ToListAsync(specification);
        }

        public async Task<Cars> UpdateAsync(Cars car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            if (car.Id <= 0)
                throw new ArgumentException("Car Id must be greater than zero", nameof(car));

            var existingCar = await Repository.GetByIdAsync<int>(car.Id);
            if (existingCar == null)
                throw new KeyNotFoundException($"Car with Id {car.Id} not found");

            existingCar.CarName = car.CarName;
            existingCar.CarType = car.CarType;
            existingCar.Manufacturer = car.Manufacturer;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.UpdatedAt = DateTime.UtcNow;

            Repository.Update(existingCar);
            await _unitOfWork.SaveChangesAsync();
            return existingCar;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero", nameof(id));

            var car = await Repository.GetByIdAsync<int>(id);
            if (car == null)
                return false;

            Repository.Delete(car);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Example method demonstrating pagination using Specification pattern
        /// </summary>
        public async Task<PagedResult<Cars>> GetPagedAsync(PaginationParams pagination, string? manufacturer = null, int? minYear = null)
        {
            var specification = new PagedCarsSpecification(pagination, manufacturer, minYear);
            return await Repository.ToPagedListAsync(specification, pagination);
        }

        /// <summary>
        /// Example method demonstrating complex query using Specification pattern
        /// </summary>
        public async Task<IEnumerable<Cars>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var specification = new CarsByPriceRangeSpecification(minPrice, maxPrice);
            return await Repository.ToListAsync(specification);
        }
    }
}
