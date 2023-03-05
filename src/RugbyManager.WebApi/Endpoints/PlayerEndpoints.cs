using AutoMapper;
using MediatR;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models;
using RugbyManager.Application.Players.Commands;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class PlayerEndpoints
{
    public static void AddPlayerEndPoints(this WebApplication app)
    {
        var player = app.MapGroup("/player");

        player.MapPost("/",
                  async (IMediator mediator, IMapper mapper, AddPlayerRequest request) =>
                      await mediator.Send(request.Transform(((IMapperBase) mapper).Map<AddPlayerCommand>))
              )
              .AddEndpointFilter<ValidationFilter<AddPlayerRequest>>()
              .WithOpenApi();

        player.MapPut("/",
                  async (IMediator mediator, IMapper mapper, UpdatePlayerRequest request) =>
                      await mediator.Send(request.Transform(mapper.Map<UpdatePlayerCommand>))
              )
              .AddEndpointFilter<ValidationFilter<UpdatePlayerRequest>>()
              .WithOpenApi();
    }
}