using SalasDeReuniaoCRUD.Domain.Common;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Enums;

namespace SalasDeReuniaoCRUD.Domain.Reservas
{
    public interface IReservaRepository : IRepository<Reserva, int>
    {
        Task<Reserva?> GetByIdAsync(int id);
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<PagedResult<Reserva>> GetPagedAsync(Status? status,
                                                DateTime? startDate,
                                                DateTime? endDate,
                                                int page,
                                                int pageSize);
    }
}
