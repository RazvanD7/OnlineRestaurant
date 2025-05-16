using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace OnlineRestaurantWpf.Data // Ensure this namespace matches your folder structure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext> // TContext is RestaurantDbContext
    {
        public RestaurantDbContext CreateDbContext(string[] args) // Method returns the correct DbContext type
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>(); // Options for the correct DbContext type

            // This connection string is used ONLY for design-time tools (migrations).
            // It should point to the database that your migrations target.
            // Based on your SSMS screenshot (image_401947.png), migrations seem to have targeted
            // "OnlineRestaurantDb" on your "SQLEXPRESS" instance.
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=OnlineRestaurantDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

            return new RestaurantDbContext(optionsBuilder.Options); // Instantiates the correct DbContext type
        }
    }
}