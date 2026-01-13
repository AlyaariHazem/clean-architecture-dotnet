namespace CleanArchitecture.API.Contracts
{
    public class CarResponseDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCarRequestDto
    {
        public string CarName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateCarRequestDto
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
    }

    // Legacy contracts for backward compatibility
    public class CarsContract
    {
        public string CarName { get; set; } = string.Empty;
    }

    public class CarsOperationsContract
    {
        public string CarName { get; set; } = string.Empty;
    }
}
