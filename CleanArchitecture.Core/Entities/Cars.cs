namespace CleanArchitecture.Core.Entities
{
    public class Cars
    {
        public int Id { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
