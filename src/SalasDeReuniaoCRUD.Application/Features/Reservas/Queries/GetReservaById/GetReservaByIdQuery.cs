using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaById;

public record GetReservaByIdQuery(int Id) : IRequest<ReservaDto>;
