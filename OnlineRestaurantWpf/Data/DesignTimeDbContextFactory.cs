using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace OnlineRestaurantWpf.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext>
    {
        public RestaurantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>();

            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=OnlineRestaurantDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

            return new RestaurantDbContext(optionsBuilder.Options);
        }
    }
}