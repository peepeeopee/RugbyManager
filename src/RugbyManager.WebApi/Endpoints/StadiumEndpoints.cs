using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Stadium;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Stadiums.Queries;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class StadiumEndpoints
{
    public static void AddStadiumEndpoints(this WebApplication app)
    {
        var stadiums = app.MapGroup("/stadiums")
                         .WithTags("Stadiums");

        stadiums.MapGet("/",
                    async (IMediator mediator, IMapper mapper) =>
                        await mediator.Send(new GetStadiumsQuery())
                )
                .WithOpenApi();

        stadiums.MapGet("/{stadiumId}",
                    async (IMediator mediator, IMapper mapper, int stadiumId) =>
                        await mediator.Send(new GetStadiumByIdQuery()
                        {
                            StadiumId = stadiumId
                        })
                )
                .AddEndpointFilter<ValidationFilter<GetStadiumByIdQuery>>()
                .WithOpenApi();

        stadiums.MapPost("/",
                   async (
                       IMediator mediator,
                       IMapper mapper,
                       [FromBody] AddStadiumRequest request) =>
                       await mediator.Send(
                           request.Transform(((IMapperBase) mapper).Map<AddStadiumCommand>))
               )
               .AddEndpointFilter<ValidationFilter<AddStadiumRequest>>()
               .WithOpenApi();

        stadiums.MapPut("/",
                    async (
                        IMediator mediator,
                        IMapper mapper,
                        [FromBody] UpdateStadiumRequest request) =>
                        await mediator.Send(
                            request.Transform(((IMapperBase)mapper).Map<UpdateStadiumCommand>))
                )
                .AddEndpointFilter<ValidationFilter<UpdateStadiumRequest>>()
                .WithOpenApi();

        stadiums.MapDelete("/{stadiumId}",
                    async (IMediator mediator, IMapper mapper, int stadiumId) =>
                        await mediator.Send(new RemoveStadiumRequest()
                        {
                            StadiumId = stadiumId
                        }.Transform(mapper.Map<RemoveStadiumCommand>))
                )
                .AddEndpointFilter<ValidationFilter<GetStadiumByIdQuery>>()
                .WithOpenApi();
    }
}