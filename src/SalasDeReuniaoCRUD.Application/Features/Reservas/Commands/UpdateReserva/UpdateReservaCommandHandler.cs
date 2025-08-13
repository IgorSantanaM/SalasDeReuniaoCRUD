using MediatR;
using SalasDeReuniaoCRUD.Application.Exceptions;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.UpdateReserva
{
    public class UpdateReservaCommandHandler : IRequestHandler<UpdateReservaCommand>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateReservaCommandHandler(IReservaRepository reservaRepository, IUnitOfWork unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateReservaCommand request, CancellationToken cancellationToken)
        {
            Reserva reserva = await _reservaRepository.GetByIdAsync(request.Id);
            if (reserva == null!)
                throw new NotFoundException(nameof(Reserva), request.Id);

            reserva.Update(request.Titulo, request.Responsavel, request.DataInicio, request.DataFim, request.ParticipantesPrevistos, request.ValorHora, request.Desconto);
            _reservaRepository.Update(reserva);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
