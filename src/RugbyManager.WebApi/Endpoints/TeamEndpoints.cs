using AutoMapper;
using MediatR;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class TeamEndpoints
{
    public static void AddTeamEndPoints(this WebApplication app)
    {
        var team = app.MapGroup("/team");

        team.MapPost("/",
                async (IMediator mediator, IMapper mapper, AddTeamRequest request) =>
                    await mediator.Send(request.Transform(mapper.Map<AddTeamCommand>))
            )
            .AddEndpointFilter<ValidationFilter<AddTeamRequest>>()
            .WithOpenApi();
    }
}