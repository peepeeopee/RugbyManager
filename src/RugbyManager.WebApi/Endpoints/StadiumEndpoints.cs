using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Stadium;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Stadiums.Queries;
using RugbyManager.Domain.Exceptions;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class StadiumEndpoints
{
    public static void AddStadiumEndpoints(this WebApplication app)
    {
        var stadiums = app.MapGroup("/stadiums")
                          .AddEndpointFilter<ApiKeyFilter>()
                          .WithTags("Stadiums");

        stadiums.MapGet("/",
                    async (IMediator mediator, IMapper mapper) =>
                        await mediator.Send(new GetStadiumsQuery())
                )
                .Produces<List<StadiumDto>>()
                .WithDescription("This endpoint provides a list of all stadiums in the system")
                .WithOpenApi();

        stadiums.MapGet("/{stadiumId}",
                    async (IMediator mediator, IMapper mapper, int stadiumId) =>
                        await mediator.Send(new GetStadiumByIdQuery()
                        {
                            StadiumId = stadiumId
                        })
                )
                .Produces<StadiumDto>()
                .ProducesValidationProblem()
                .WithDescription("This endpoint provides access to the details of stadium that matches the supplied Id")
                .WithOpenApi();

        stadiums.MapPost("/",
                    async (
                        IMediator mediator,
                        IMapper mapper,
                        [FromBody] AddStadiumRequest request) =>
                        await mediator.Send(
                            request.Transform(mapper.Map<AddStadiumCommand>))
                )
                .Produces(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .WithDescription("This endpoint provides access to add a new stadium to the system")
                .AddEndpointFilter<ValidationFilter<AddStadiumRequest>>()
                .WithOpenApi();

        stadiums.MapPut("/",
                    async (
                        IMediator mediator,
                        IMapper mapper,
                        [FromBody] UpdateStadiumRequest request) =>
                        await mediator.Send(
                            request.Transform(mapper.Map<UpdateStadiumCommand>))
                )
                .WithDescription("This endpoint provides access to update an existing stadium that matches the supplied Id")
                .Produces(StatusCodes.Status200OK)
                .Produces<StadiumNotFoundException>(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .AddEndpointFilter<ValidationFilter<UpdateStadiumRequest>>()
                .WithOpenApi();

        stadiums.MapDelete("/{stadiumId}",
                    async (IMediator mediator, IMapper mapper, int stadiumId) =>
                        await mediator.Send(new RemoveStadiumRequest()
                        {
                            StadiumId = stadiumId
                        }.Transform(mapper.Map<RemoveStadiumCommand>))
                )
                .WithDescription("This endpoint provides access to remove an existing stadium that matches the supplied Id")
                .Produces(StatusCodes.Status200OK)
                .Produces<StadiumNotFoundException>(StatusCodes.Status500InternalServerError)
                .Produces<StadiumInUseException>(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithOpenApi();
    }
}