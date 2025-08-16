using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Application.Features.Reservas.Commands.PatchReserva;

public record PatchReservaCommand(int Id, decimal Desconto) : IRequest<ReservaDto>;