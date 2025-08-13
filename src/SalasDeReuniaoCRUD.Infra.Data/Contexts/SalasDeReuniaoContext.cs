using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Infra.Data.Contexts
{
    public class SalasDeReuniaoContext : DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }

        public SalasDeReuniaoContext(DbContextOptions<SalasDeReuniaoContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalasDeReuniaoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
