using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SalasDeReuniaoCRUD.Infra.Data.Contexts;

namespace SalasDeReuniaoCRUD.Infra.Data.Factories
{
    public class SalasDeReuniaoContextFactory : IDesignTimeDbContextFactory<SalasDeReuniaoContext>
    {
        public SalasDeReuniaoContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SalasDeReuniaoContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named 'Postgres'.");
            }

            optionsBuilder.UseNpgsql(connectionString);

            return new SalasDeReuniaoContext(optionsBuilder.Options);
        }
    }
}
