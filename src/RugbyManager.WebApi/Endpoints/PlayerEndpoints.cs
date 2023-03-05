using AutoMapper;
using MediatR;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Player;
using RugbyManager.Application.Players.Commands;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class PlayerEndpoints
{
    public static void AddPlayerEndPoints(this WebApplication app)
    {
        var player = app.MapGroup("/player")
                        .AddEndpointFilter<ValidationFilter<AddPlayerRequest>>();

        player.MapPost("/",
                  async (IMediator mediator, IMapper mapper, AddPlayerRequest request) =>
                      await mediator.Send(
                          request.Transform(mapper.Map<AddPlayerCommand>))
              )
              .WithOpenApi();

        player.MapPut("/",
                  async (IMediator mediator, IMapper mapper, UpdatePlayerRequest request) =>
                      await mediator.Send(
                          request.Transform(mapper.Map<UpdatePlayerCommand>))
              )
              .WithOpenApi();

        player.MapDelete("/",
                  async (IMediator mediator, IMapper mapper, RemovePlayerRequest request) =>
                      await mediator.Send(
                          request.Transform(mapper.Map<RemovePlayerCommand>))
              )
              .WithOpenApi();
    }
}