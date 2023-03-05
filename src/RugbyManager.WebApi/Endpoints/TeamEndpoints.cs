using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Team;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Application.Teams.Queries;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class TeamEndpoints
{
    public static void AddTeamEndPoints(this WebApplication app)
    {
        var team = app.MapGroup("/teams")
                      .WithTags("Teams");

        team.MapGet("/",
                async (IMediator mediator, IMapper Mapper) =>
                    await mediator.Send(new GetTeamsQuery())
            )
            .WithOpenApi();

        team.MapGet("/{teamId}",
                async (IMediator mediator, IMapper Mapper, int teamId) =>
                    await mediator.Send(new GetTeamByIdQuery()
                    {
                        TeamId = teamId
                    })
            )
            .AddEndpointFilter<ValidationFilter<GetTeamByIdQuery>>()
            .WithOpenApi();

        team.MapPost("/",
                async (IMediator mediator, IMapper mapper, [FromBody] AddTeamRequest request) =>
                    await mediator.Send(request.Transform(mapper.Map<AddTeamCommand>))
            )
            .AddEndpointFilter<ValidationFilter<AddTeamRequest>>()
            .WithOpenApi();

        team.MapPut("/",
            async (IMediator mediator, IMapper mapper, [FromBody] UpdateTeamRequest request) =>
                await mediator.Send(request.Transform(mapper.Map<UpdateTeamCommand>))
                )
            .AddEndpointFilter<ValidationFilter<UpdateTeamRequest>>()
            .WithOpenApi();

        team.MapDelete("/{teamId}",
                async (IMediator mediator, IMapper mapper, int teamId) =>
                    await mediator.Send(new RemoveTeamRequest()
                    {
                        TeamId = teamId
                    })
            )
            .AddEndpointFilter<ValidationFilter<RemoveTeamRequest>>()
            .WithOpenApi();
    }
}