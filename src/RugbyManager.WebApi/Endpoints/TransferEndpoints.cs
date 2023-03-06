using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Transfers;
using RugbyManager.Application.Transfers.Commands;
using RugbyManager.Application.Transfers.Queries;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class TransferEndpoints
{
    public static void AddTransferEndpoints(this WebApplication app)
    {
        var transfers = app.MapGroup("/transfers")
                           .AddEndpointFilter<ApiKeyFilter>()
                           .WithTags("Transfers");

        transfers.MapPost("/",
                     async (
                         IMediator mediator,
                         IMapper mapper,
                         [FromBody] TransferPlayerRequest request) =>
                         await mediator.Send(
                             request.Transform(((IMapperBase) mapper).Map<TransferPlayerCommand>))
                 )
                 .AddEndpointFilter<ValidationFilter<TransferPlayerRequest>>()
                 .WithDescription("Use this endpoint to transfer a player to a new team. Can also be used to allocate a team to free agent")
                 .WithOpenApi();

        transfers.MapGet("/",
                     async (
                         IMediator mediator,
                         IMapper mapper) =>
                         await mediator.Send(new GetTransfersQuery()))
                 .Produces<List<TransferDto>>()
                 .WithDescription("This endpoint provides a list of all stadiums in the system")
                 .WithOpenApi();
    }
}