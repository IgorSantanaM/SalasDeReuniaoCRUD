using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaByStatus
{
    public class GetReservaByStatusQueryHandler : IRequestHandler<GetReservaByStatusQuery, IEnumerable<ReservaDto>>
    {
        private readonly IReservaRepository _reservaRepository;
        public GetReservaByStatusQueryHandler(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<ReservaDto>> Handle(GetReservaByStatusQuery request, CancellationToken cancellationToken)
        {
            var reservas = await _reservaRepository.GetReservasByStatus(request.Status);
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
