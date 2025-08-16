using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.CreateReserva;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.DeleteReserva;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Commands.UpdateReserva;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetAllReservas;
using SalaDeReuniaoCRUD.Application.Features.Reservas.Queries.GetReservaById;
using SalasDeReuniaoCRUD.Application.Features.Reservas.Commands.PatchReserva;
using SalasDeReuniaoCRUD.Domain.Enums;
using SalasDeReuniaoCRUD.WebApi.Endpoints.Internal;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace SalasDeReuniaoCRUD.WebApi.Endpoints
{
    public class ReservaEndpoints : IEndpoints
    {
        public static void DefineEndpoint(WebApplication app)
        {
            var group = app.MapGroup("/api/reservas").WithTags("Reservas");

            group.MapPost("/", HandleCreateReserva)
                .WithName("CreateReserva")
                .Produces<int>(StatusCodes.Status201Created)
                .Produces<object>(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Cria uma nova reserva")
                .WithDescription("Cria uma nova reserva com os detalhes. Retorna o Id da reserva se sucesso.");

            group.MapGet("/{id:int}", HandleGetReservaById)
                .WithName("GetReservaById")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Obtém uma reserva por Id")
                .WithDescription("Obtém os detalhes de uma reserva específica pelo Id.");

            group.MapGet("/", HandleGetReservas)
                .WithName("GetReservas")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Obtém todas as reservas com seus filtros de range de data, status e paginação.")
                .WithDescription("Obtém uma lista de todas as reservas.");

            group.MapDelete("/{id:int}", HandleDeleteReserva)
                .WithName("DeleteReserva")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Exclui uma reserva")
                .WithDescription("Exclui uma reserva específica pelo Id.");

            group.MapPut("/", HandleUpdateReserva)
                .WithName("UpdateReserva")
                .Produces(StatusCodes.Status204NoContent)
                .Produces<object>(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Atualiza uma reserva")
                .WithDescription("Atualiza os detalhes de uma reserva existente.");

            group.MapPatch("/{id:int}/desconto", HandlePatchDesconto)
                .WithName("PatchDescontoReserva")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Atualiza o desconto de uma reserva")
                .WithDescription("Atualiza o valor do desconto de uma reserva específica pelo Id.");
        }

        #region Handlers

        private static async Task<IResult> HandleCreateReserva(
            [FromBody] CreateRersevaCommand command, [FromServices] IMediator mediator)
        {
            var id = await mediator.Send(command);

            return Results.CreatedAtRoute("GetReservaById",
                new { id },
                null);
        }

        private static async Task<IResult> HandleGetReservaById(
            [FromRoute] int id, [FromServices] IMediator mediator)
        {
            var query = new GetReservaByIdQuery(id);
            var reserva = await mediator.Send(query);
            return reserva is not null
                ? Results.Ok(reserva)
                : Results.NotFound();
        }

        private static async Task<IResult> HandleGetReservas(
            [FromServices] IMediator mediator,
             [FromQuery] int page = 1,
             [FromQuery] int pageSize = 10,
             [FromQuery] Status? status = null,
             [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var query = new GetAllReservasQuery(status, startDate, endDate, page, pageSize);
            var reservas = await mediator.Send(query);
            return Results.Ok(reservas);
        }

        private static async Task<IResult> HandleDeleteReserva(
            [FromRoute] int id, [FromServices] IMediator mediator)
        {
            var command = new DeleteReservaCommand(id);
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandleUpdateReserva([FromBody] UpdateReservaCommand command, [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
            return Results.NoContent();
        }

        private static async Task<IResult> HandlePatchDesconto(
            [FromRoute] int id, [FromBody] decimal desconto, [FromServices] IMediator mediator)
        {
            var command = new PatchReservaCommand(id, desconto);
            var response = await mediator.Send(command);
            return Results.Ok(response);
        }

        #endregion
    }
}
