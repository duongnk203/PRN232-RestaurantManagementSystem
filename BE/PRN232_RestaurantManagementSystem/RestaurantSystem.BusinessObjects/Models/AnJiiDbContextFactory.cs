using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RestaurantSystem.BusinessObjects.Models
{
    public class AnJiiDbContextFactory : IDesignTimeDbContextFactory<AnJiiDbContext>
    {
        public AnJiiDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // thư mục đang chạy EF
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AnJiiDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new AnJiiDbContext(optionsBuilder.Options);
        }
    }
}
