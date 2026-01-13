namespace CleanArchitecture.Core.Entities
{
    /// <summary>
    /// Car entity inheriting from BaseEntity for common audit fields
    /// </summary>
    public class Cars : BaseEntity
    {
        public string CarName { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}
