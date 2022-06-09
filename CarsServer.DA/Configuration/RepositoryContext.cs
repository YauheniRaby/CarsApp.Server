using CarsServer.DA.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsServer.DA.Configuration
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
