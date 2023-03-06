using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Team;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Application.Teams.Queries;
using RugbyManager.Domain.Exceptions;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class TeamEndpoints
{
    public static void AddTeamEndPoints(this WebApplication app)
    {
        var team = app.MapGroup("/teams")
                      .AddEndpointFilter<ApiKeyFilter>()
                      .WithTags("Teams");

        team.MapGet("/",
                async (IMediator mediator, IMapper Mapper) =>
                    await mediator.Send(new GetTeamsQuery())
            )
            .Produces<List<TeamDto>>()
            .WithDescription("This endpoint provides a list of all teams in the system")
            .WithOpenApi();

        team.MapGet("/{teamId}",
                async (IMediator mediator, IMapper Mapper, int teamId) =>
                    await mediator.Send(new GetTeamByIdQuery()
                    {
                        TeamId = teamId
                    })
            )
            .AddEndpointFilter<ValidationFilter<GetTeamByIdQuery>>()
            .Produces<TeamDto>()
            .ProducesValidationProblem()
            .WithDescription("This endpoint provides access to the details of team that matches the supplied Id")
            .WithOpenApi();

        team.MapPost("/",
                async (IMediator mediator, IMapper mapper, [FromBody] AddTeamRequest request) =>
                    await mediator.Send(request.Transform(mapper.Map<AddTeamCommand>))
            )
            .AddEndpointFilter<ValidationFilter<AddTeamRequest>>()
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithDescription("This endpoint provides access to add a new team to the system")
            .WithOpenApi();

        team.MapPut("/",
                async (IMediator mediator, IMapper mapper, [FromBody] UpdateTeamRequest request) =>
                    await mediator.Send(request.Transform(mapper.Map<UpdateTeamCommand>))
            )
            .AddEndpointFilter<ValidationFilter<UpdateTeamRequest>>()
            .WithDescription("This endpoint provides access to update an existing team that matches the supplied Id")
            .Produces(StatusCodes.Status200OK)
            .Produces<PlayerNotFoundException>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

        team.MapDelete("/{teamId}",
                async (IMediator mediator, IMapper mapper, int teamId) =>
                    await mediator.Send(new RemoveTeamRequest()
                    {
                        TeamId = teamId
                    }.Transform(mapper.Map<RemoveTeamCommand>))
            )
            .AddEndpointFilter<ValidationFilter<RemoveTeamRequest>>()
            .WithDescription("This endpoint provides access to remove an existing team that matches the supplied Id")
            .Produces(StatusCodes.Status200OK)
            .Produces<PlayerNotFoundException>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
    }
}