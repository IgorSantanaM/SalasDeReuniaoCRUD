using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.DeleteReserva;

public record DeleteReservaCommand(int Id) : IRequest;
