using MediatR;
using SalasDeReuniaoCRUD.Application.Exceptions;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.DeleteReserva
{
    public class DeleteReservaCommandHandler : IRequestHandler<DeleteReservaCommand>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteReservaCommandHandler(IReservaRepository reservaRepository, IUnitOfWork unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteReservaCommand request, CancellationToken cancellationToken)
        {
            var reserva = await _reservaRepository.GetByIdAsync(request.Id);
            if (reserva == null!)
                throw new NotFoundException(nameof(Reserva), request.Id);

            _reservaRepository.Delete(reserva);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
