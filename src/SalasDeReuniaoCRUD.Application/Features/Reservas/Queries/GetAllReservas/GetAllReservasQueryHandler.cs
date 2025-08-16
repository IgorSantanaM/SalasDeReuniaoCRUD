using MediatR;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos;
using SalasDeReuniaoCRUD.Domain.Common;
using SalasDeReuniaoCRUD.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetAllReservas
{
    public class GetAllReservasQueryHandler : IRequestHandler<GetAllReservasQuery, PagedResult<ReservaDto>>
    {
        private readonly IReservaRepository _reservaRepository;

        public GetAllReservasQueryHandler(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }
        public async Task<PagedResult<ReservaDto>> Handle(GetAllReservasQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _reservaRepository.GetPagedAsync(request.Status, request.StartDate, request.EndDate, request.Page, request.PageSize);
            var reservaDtos = pagedResult.Items.Select(r => new ReservaDto(
                r.Id,
                r.Titulo,
                r.Responsavel,
                r.DataInicio,
                r.DataFim,
                r.ParticipantesPrevistos,
                r.ValorHora,
                r.Desconto,
                r.ValorTotal
            ));

            return new PagedResult<ReservaDto>
            {
                Items = reservaDtos,
                TotalPages= pagedResult.TotalPages,
                TotalCount= pagedResult.TotalCount,
            };
        }
    }
}
