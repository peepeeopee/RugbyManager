using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Transfers;
using RugbyManager.Application.Transfers.Commands;
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
                 .WithOpenApi();
    }
}