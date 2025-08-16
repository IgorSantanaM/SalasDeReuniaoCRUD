using Dapper;
using Microsoft.EntityFrameworkCore;
using SalasDeReuniaoCRUD.Domain.Common;
using SalasDeReuniaoCRUD.Domain.Enums;
using SalasDeReuniaoCRUD.Domain.Reservas;
using SalasDeReuniaoCRUD.Infra.Data.Contexts;
using System.Data;
using System.Text;

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

        public async Task<PagedResult<Reserva>> GetPagedAsync(Status? status, DateTime? startDate, DateTime? endDate, int page, int pageSize)
        {
            var parameters = new DynamicParameters();
            var whereClauses = new List<string>();

            var countSql = new StringBuilder("SELECT COUNT(*) FROM \"Reservas\"");
            var selectSql = new StringBuilder("SELECT * FROM \"Reservas\"");

            if (startDate.HasValue)
            {
                whereClauses.Add("\"DataInicio\" >= @StartDate");
                parameters.Add("StartDate", startDate.Value);
            }

            if (endDate.HasValue)
            {
                whereClauses.Add("\"DataFim\" <= @EndDate");
                parameters.Add("EndDate", endDate.Value);
            }

                var now = DateTime.Now;
                parameters.Add("Now", now);

                switch (status)
                {
                    case Status.EmAndamento:
                        whereClauses.Add("(@Now >= \"DataInicio\" AND @Now <= \"DataFim\")");
                        break;
                    case Status.FuturasProximas:
                        whereClauses.Add("(\"DataInicio\" > @Now AND \"DataInicio\" <= @TwentyFourHoursFromNow)");
                        parameters.Add("TwentyFourHoursFromNow", now.AddHours(24));
                        break;
                    case Status.FuturasNormais:
                        whereClauses.Add("(\"DataInicio\" > @TwentyFourHoursFromNow)");
                        parameters.Add("TwentyFourHoursFromNow", now.AddHours(24));
                        break;
                    case Status.Encerradas:
                        whereClauses.Add("(\"DataFim\" < @Now)");
                        break;
                }

            if (whereClauses.Any())
            {
                var whereSql = " WHERE " + string.Join(" AND ", whereClauses);
                countSql.Append(whereSql);
                selectSql.Append(whereSql);
            }

            selectSql.Append(" ORDER BY \"DataInicio\" DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            parameters.Add("Offset", (page - 1) * pageSize);
            parameters.Add("PageSize", pageSize);

            using (var multi = await _dbConnection.QueryMultipleAsync(countSql.ToString() + ";" + selectSql.ToString(), parameters))
            {
                var totalCount = await multi.ReadSingleAsync<int>();
                var items = (await multi.ReadAsync<Reserva>()).ToList();

                return new PagedResult<Reserva>
                {
                    Items = items,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
            }
        }

    }
}
