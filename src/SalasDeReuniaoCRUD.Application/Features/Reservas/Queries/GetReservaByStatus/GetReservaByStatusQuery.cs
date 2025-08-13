using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaByStatus;

public record GetReservaByStatusQuery(Status Status) : IRequest<IEnumerable<ReservaDto>>;
