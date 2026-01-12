using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastracture.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }
    }
}
