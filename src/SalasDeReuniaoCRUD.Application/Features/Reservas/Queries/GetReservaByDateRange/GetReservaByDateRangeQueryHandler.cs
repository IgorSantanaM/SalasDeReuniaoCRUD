using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaByDataRange
{
    public class GetReservaByDateRangeQueryHandler : IRequestHandler<GetReservaByDateRangeQuery, IEnumerable<ReservaDto>>
    {
        private readonly IReservaRepository _reservaRepository;
        public GetReservaByDateRangeQueryHandler(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }
        public async Task<IEnumerable<ReservaDto>> Handle(GetReservaByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var reservas = await _reservaRepository.GetReservasByDateRangeAsync(request.DataInicio, request.DataFim);
            var reservaDtos = reservas.Select(r => new ReservaDto(
                r.Id,
                r.Titulo,
                r.Responsavel,
                r.DataInicio,
                r.DataFim,
                r.ParticipantesPrevistos,
                r.ValorHora,
                r.Desconto,
                r.ValorTotal
            ));
            return reservaDtos;
        }
    }
}
