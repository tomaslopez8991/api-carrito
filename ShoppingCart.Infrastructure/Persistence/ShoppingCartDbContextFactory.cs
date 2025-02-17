using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ShoppingCart.Infrastructure.Persistence
{
    public class ShoppingCartDbContextFactory : IDesignTimeDbContextFactory<ShoppingCartDbContext>
    {
        public ShoppingCartDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Api-carrito_test"));
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ShoppingCartDbContext(optionsBuilder.Options);
        }
    }
}
