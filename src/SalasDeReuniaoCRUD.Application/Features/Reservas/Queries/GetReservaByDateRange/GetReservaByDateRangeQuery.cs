using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaByDataRange
{
    public record GetReservaByDateRangeQuery(
        DateTime DataInicio,
        DateTime DataFim) : IRequest<IEnumerable<ReservaDto>>
    {
    }
}
