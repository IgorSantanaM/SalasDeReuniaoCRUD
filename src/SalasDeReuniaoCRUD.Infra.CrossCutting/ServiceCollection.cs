using Microsoft.Extensions.DependencyInjection;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.CreateReserva;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Validators;
using FluentValidation;
using SalasDeReuniaoCRUD.Domain.Reservas;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Infra.Data.Repositories;
using SalasDeReuniaoCRUD.Infra.Data.UoW;
using System.Data;
using SalasDeReuniaoCRUD.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SalasDeReuniaoCRUD.Infra.CrossCutting
{
    public static class ServiceCollection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Application
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateReservaCommandHandler).Assembly)
            );

            services.AddScoped<IDbConnection>(sp =>
               sp.GetRequiredService<SalasDeReuniaoContext>().Database.GetDbConnection());

            services.AddValidatorsFromAssembly(typeof(CreateReservaCommandValidator).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IReservaRepository, ReservaRepository>();
        }
    }
}
