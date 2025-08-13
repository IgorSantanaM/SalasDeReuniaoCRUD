using FluentValidation;
using MediatR;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Reservas;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.CreateReserva
{
    public class CreateReservaCommandHandler : IRequestHandler<CreateRersevaCommand, int>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateRersevaCommand> _validator;
        public CreateReservaCommandHandler(IReservaRepository reservaRepository, IUnitOfWork unitOfWork, IValidator<CreateRersevaCommand> validator)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<int> Handle(CreateRersevaCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var reserva = new Reserva(request.Titulo, request.Responsavel,request.DataInicio, request.DataFim, request.ParticipantesPrevistos, request.ValorHora, request.Desconto);

            await _reservaRepository.AddAsync(reserva);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reserva.Id;
        }
    }
}
