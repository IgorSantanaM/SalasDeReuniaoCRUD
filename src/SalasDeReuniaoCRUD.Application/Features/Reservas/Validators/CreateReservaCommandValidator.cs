using FluentValidation;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.CreateReserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Application.Features.Reservas.Validators
{
    public class CreateReservaCommandValidator : AbstractValidator<CreateRersevaCommand>
    {
        public CreateReservaCommandValidator()
        {
            RuleFor(command => command.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.");

            RuleFor(command => command.Responsavel)
                .NotEmpty().WithMessage("O nome do responsável é obrigatório.");

            RuleFor(command => command.DataInicio)
                .GreaterThan(DateTime.Now).WithMessage("A data de início não pode ser no passado.");

            RuleFor(command => command.DataFim)
                .GreaterThan(command => command.DataInicio)
                .WithMessage("A data de fim deve ser posterior à data de início.");

            RuleFor(command => command.ParticipantesPrevistos)
                .GreaterThan(0).WithMessage("O número de participantes previstos deve ser maior que zero.");

            RuleFor(command => command.ValorHora)
                .GreaterThanOrEqualTo(0).WithMessage("O valor por hora não pode ser negativo.");

            RuleFor(command => command.Desconto)
                .GreaterThanOrEqualTo(0).WithMessage("O valor do desconto não pode ser negativo.")
                .LessThanOrEqualTo(30).WithMessage("O valor do desconto não pode ser maior que 30%.");
        }
    }
}
