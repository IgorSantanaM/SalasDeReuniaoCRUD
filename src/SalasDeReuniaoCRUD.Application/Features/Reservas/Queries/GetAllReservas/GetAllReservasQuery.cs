using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Common;
using SalasDeReuniaoCRUD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetAllReservas
{
    public record GetAllReservasQuery(Status? Status,
                                    DateTime? StartDate,
                                    DateTime? EndDate,
                                    int Page,
                                    int PageSize) :IRequest<PagedResult<ReservaDto>>
    {
    }
}
