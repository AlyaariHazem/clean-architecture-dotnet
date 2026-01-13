using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Specifications;

namespace CleanArchitecture.Core.Specifications
{
    /// <summary>
    /// Example specifications for Car entity demonstrating the Specification pattern
    /// </summary>
    
    public class CarByIdSpecification : BaseSpecification<Cars>
    {
        public CarByIdSpecification(int id)
        {
            Criteria = c => c.Id == id;
            EnableTracking(false); // Read-only, no tracking needed
        }
    }

    public class CarsByManufacturerSpecification : BaseSpecification<Cars>
    {
        public CarsByManufacturerSpecification(string manufacturer)
        {
            Criteria = c => c.Manufacturer == manufacturer;
            ApplyOrderBy(c => c.Year);
        }
    }

    public class CarsByPriceRangeSpecification : BaseSpecification<Cars>
    {
        public CarsByPriceRangeSpecification(decimal minPrice, decimal maxPrice)
        {
            Criteria = c => c.Price >= minPrice && c.Price <= maxPrice;
            ApplyOrderByDescending(c => c.Price);
        }
    }

    public class CarsByYearSpecification : BaseSpecification<Cars>
    {
        public CarsByYearSpecification(int year)
        {
            Criteria = c => c.Year == year;
            ApplyOrderBy(c => c.Manufacturer);
            ApplyThenBy(c => c.CarName);
        }
    }

    public class PagedCarsSpecification : BaseSpecification<Cars>
    {
        public PagedCarsSpecification(PaginationParams pagination, string? manufacturer = null, int? minYear = null)
        {
            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                Criteria = c => c.Manufacturer == manufacturer;
            }

            if (minYear.HasValue)
            {
                var existingCriteria = Criteria;
                Criteria = existingCriteria == null
                    ? c => c.Year >= minYear.Value
                    : c => existingCriteria.Compile()(c) && c.Year >= minYear.Value;
            }

            ApplyOrderByDescending(c => c.CreatedAt);
            ApplyPaging(pagination.Skip, pagination.Take);
        }
    }

    public class AllCarsSpecification : BaseSpecification<Cars>
    {
        public AllCarsSpecification()
        {
            // No criteria - returns all cars
            ApplyOrderBy(c => c.Manufacturer);
            ApplyThenBy(c => c.Year);
        }
    }
}
