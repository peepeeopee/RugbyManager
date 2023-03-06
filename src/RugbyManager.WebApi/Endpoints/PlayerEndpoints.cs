using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Player;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Players.Queries;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class PlayerEndpoints
{
    public static void AddPlayerEndPoints(this WebApplication app)
    {
        var player = app.MapGroup("/players")
                        .AddEndpointFilter<ApiKeyFilter>()
                        .WithTags("Players");

        player.MapGet("/",
                  async (IMediator mediator, IMapper mapper) =>
                      await mediator.Send(new GetPlayersQuery())
              )
              .WithOpenApi();

        player.MapGet("/{playerId}",
                  async (IMediator mediator, IMapper mapper, int playerId) =>
                      await mediator.Send(new GetPlayerByIdQuery()
                      {
                          PlayerId = playerId
                      })
              )
              .AddEndpointFilter<ValidationFilter<GetPlayersQuery>>()
              .WithOpenApi();

        player.MapPost("/",
                  async (IMediator mediator, IMapper mapper, [FromBody] AddPlayerRequest request) =>
                      await mediator.Send(
                          request.Transform(mapper.Map<AddPlayerCommand>))
              )
              .AddEndpointFilter<ValidationFilter<AddPlayerRequest>>()
              .WithOpenApi();

        player.MapPut("/",
                  async (IMediator mediator, IMapper mapper, [FromBody] UpdatePlayerRequest request) =>
                      await mediator.Send(
                          request.Transform(mapper.Map<UpdatePlayerCommand>))
              )
              .AddEndpointFilter<ValidationFilter<UpdatePlayerRequest>>()
              .WithOpenApi();

        player.MapDelete("/{playerId}",
                  async (IMediator mediator, IMapper mapper, int playerId) =>
                      await mediator.Send(new RemovePlayerRequest()
                      {
                          PlayerId = playerId
                      }.Transform(mapper.Map<RemovePlayerCommand>))
              )
              .AddEndpointFilter<ValidationFilter<RemovePlayerRequest>>()
              .WithOpenApi();
    }
}