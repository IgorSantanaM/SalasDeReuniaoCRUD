using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Core.Model;
using SalasDeReuniaoCRUD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Domain.Reservas
{
    public interface IReservaRepository : IRepository<Reserva, int>
    {
        Task<Reserva?> GetByIdAsync(int id);
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<IEnumerable<Reserva>> GetReservasByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Reserva>> GetReservasByStatus(Status status);
    }
}
