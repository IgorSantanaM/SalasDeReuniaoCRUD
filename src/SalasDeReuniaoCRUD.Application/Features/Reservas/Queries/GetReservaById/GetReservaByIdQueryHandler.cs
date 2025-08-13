using MediatR;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaById;
using SalasDeReuniaoCRUD.Application.Exceptions;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Reservas;

namespace SalasDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaById
{
    public class GetReservaByIdQueryHandler : IRequestHandler<GetReservaByIdQuery, ReservaDto>
    {
        private readonly IReservaRepository _reservaRepository;

        public GetReservaByIdQueryHandler(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }
        public async Task<ReservaDto> Handle(GetReservaByIdQuery request, CancellationToken cancellationToken)
        {
            var reserva = await _reservaRepository.GetByIdAsync(request.Id);
            if (reserva == null!)
                throw new NotFoundException(nameof(Reserva), request.Id);

            return new ReservaDto(
                reserva.Id,
                reserva.Titulo,
                reserva.Responsavel,
                reserva.DataInicio,
                reserva.DataFim,
                reserva.ParticipantesPrevistos,
                reserva.ValorHora,
                reserva.Desconto,
                reserva.ValorTotal
            );
        }
    }
}
