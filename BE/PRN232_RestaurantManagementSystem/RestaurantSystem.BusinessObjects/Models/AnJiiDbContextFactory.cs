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
            // Get the directory path where BusinessObjects project is located
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = Directory.GetParent(currentDir).FullName;
            var apiSettingsPath = Path.Combine(projectDir, "RestaurantSystem.API");

            if (!Directory.Exists(apiSettingsPath))
            {
                throw new DirectoryNotFoundException($"API project directory not found at: {apiSettingsPath}");
            }

            var appsettingsPath = Path.Combine(apiSettingsPath, "appsettings.json");
            if (!File.Exists(appsettingsPath))
            {
                throw new FileNotFoundException($"appsettings.json not found at: {appsettingsPath}");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiSettingsPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AnJiiDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new AnJiiDbContext(optionsBuilder.Options);
        }
    }
}
