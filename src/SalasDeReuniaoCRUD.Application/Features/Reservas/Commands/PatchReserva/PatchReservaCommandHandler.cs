using MediatR;
using SalasDeReuniaoCRUD.Application.Exceptions;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Application.Features.Reservas.Commands.PatchReserva
{
    public class PatchReservaCommandHandler : IRequestHandler<PatchReservaCommand, ReservaDto>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchReservaCommandHandler(IReservaRepository reservaRepository, IUnitOfWork unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservaDto> Handle(PatchReservaCommand request, CancellationToken cancellationToken)
        {
            Reserva? reserva = await _reservaRepository.GetByIdAsync(request.Id);
            if (reserva == null!)
                throw new NotFoundException(nameof(Reserva), request.Id);

            reserva.AplicarDesconto(request.Desconto);
            _reservaRepository.Update(reserva);
            await _unitOfWork.SaveChangesAsync();
            return new ReservaDto(reserva.Id,
                                reserva.Titulo,
                                reserva.Responsavel,
                                reserva.DataInicio, 
                                reserva.DataFim,
                                reserva.ParticipantesPrevistos, 
                                reserva.ValorTotal,
                                reserva.Desconto,
                                reserva.ValorTotal);
        }
    }
}
