using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.CreateReserva
{
    public record CreateRersevaCommand(string Titulo,
        string Responsavel,
        DateTime DataInicio,
        DateTime DataFim,
        int ParticipantesPrevistos,
        decimal ValorHora, 
        decimal Desconto, 
        decimal ValorTotal) : IRequest<int>;
}
