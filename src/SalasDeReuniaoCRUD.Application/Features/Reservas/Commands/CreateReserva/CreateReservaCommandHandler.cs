using Dapper;
using FluentValidation;
using MediatR;
using SalasDeReuniaoCRUD.Application.Exceptions;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Core.Model;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System.Data;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.CreateReserva
{
    public class CreateReservaCommandHandler : IRequestHandler<CreateRersevaCommand, int>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnection _dbConnection;
        private readonly IValidator<CreateRersevaCommand> _validator;
        public CreateReservaCommandHandler(IReservaRepository reservaRepository, IUnitOfWork unitOfWork, IValidator<CreateRersevaCommand> validator, IDbConnection dbConnection)
        {
            _reservaRepository = reservaRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _dbConnection = dbConnection;
        }
        public async Task<int> Handle(CreateRersevaCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var parameters = new
            {
                DataInicio = request.DataInicio.ToLocalTime(),
                DataFim = request.DataFim.ToLocalTime(),
            };

            var sql = @"
                    SELECT 1
                    FROM ""Reservas""
                    WHERE
                        (""DataInicio"" < @DataFim AND ""DataFim"" > @DataInicio)
                    LIMIT 1;
                ";

            var conflito = await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, parameters);

            if (conflito == 1)
            {
                throw new ValidException("Não é possível agendar. Já existe uma reserva para esta sala no horário solicitado.");
            }

            var reserva = new Reserva(request.Titulo, request.Responsavel,request.DataInicio, request.DataFim, request.ParticipantesPrevistos, request.ValorHora, request.Desconto);

            reserva.DataInicio = reserva.DataInicio.ToLocalTime();
            reserva.DataFim = reserva.DataFim.ToLocalTime();

            await _reservaRepository.AddAsync(reserva);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reserva.Id;
        }
    }
}
