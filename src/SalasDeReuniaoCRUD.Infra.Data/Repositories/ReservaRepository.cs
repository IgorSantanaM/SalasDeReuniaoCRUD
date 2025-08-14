using Dapper;
using Microsoft.EntityFrameworkCore;
using SalasDeReuniaoCRUD.Domain.Enums;
using SalasDeReuniaoCRUD.Domain.Reservas;
using SalasDeReuniaoCRUD.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Infra.Data.Repositories
{
    public class ReservaRepository : Repository<Reserva, int>, IReservaRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly SalasDeReuniaoContext _context;
        public ReservaRepository(SalasDeReuniaoContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            const string sql = @"SELECT * FROM ""Reservas""";
            return await _dbConnection.QueryAsync<Reserva>(sql);
        }

        public async Task<Reserva?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT * FROM ""Reservas"" WHERE ""Id"" = @Id;";
            return await _dbConnection.QuerySingleOrDefaultAsync<Reserva>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Reserva>> GetReservasByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"SELECT * FROM ""Reservas"" WHERE ""DataInicio"" >= @StartDate AND ""DataFim"" <= @EndDate;";
            return await _dbConnection.QueryAsync<Reserva>(sql, new { StartDate = startDate, EndDate = endDate });
        }

        public async Task<IEnumerable<Reserva>> GetReservasByStatus(Status status)
        {
            var now = DateTime.Now;
            string sql;
            object parameters;

            switch (status)
            {
                case Status.EmAndamento:
                    sql = @"SELECT * FROM ""Reservas"" WHERE @Now >= ""DataInicio"" AND @Now <= ""DataFim"";";
                    parameters = new { Now = now };
                    break;

                case Status.FuturasProximas:
                    sql = @"SELECT * FROM ""Reservas"" WHERE ""DataInicio"" > @Now AND ""DataInicio"" <= @TwentyFourHoursFromNow;";
                    parameters = new { Now = now, TwentyFourHoursFromNow = now.AddHours(24) };
                    break;

                case Status.FuturasNormais:
                    sql = @"SELECT * FROM ""Reservas"" WHERE ""DataInicio"" > @TwentyFourHoursFromNow;";
                    parameters = new { TwentyFourHoursFromNow = now.AddHours(24) };
                    break;

                case Status.Encerradas:
                    sql = @"SELECT * FROM ""Reservas"" WHERE ""DataFim"" < @Now;";
                    parameters = new { Now = now };
                    break;

                default:
                    return Enumerable.Empty<Reserva>();
            }

            return await _dbConnection.QueryAsync<Reserva>(sql, parameters);
        }
    }
}
