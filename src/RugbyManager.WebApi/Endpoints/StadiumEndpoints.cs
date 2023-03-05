using AutoMapper;
using MediatR;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Models.Stadium;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.WebApi.Filters;

namespace RugbyManager.WebApi.Endpoints;

public static class StadiumEndpoints
{
    public static void AddStadiumEndpoints(this WebApplication app)
    {
        var stadium = app.MapGroup("/stadium")
                         .AddEndpointFilter<ValidationFilter<AddStadiumRequest>>();

        stadium.MapPost("/",
                   async (IMediator mediator, IMapper mapper, AddStadiumRequest request) =>
                       await mediator.Send(request.Transform(((IMapperBase) mapper).Map<AddStadiumCommand>))
               )
               .WithOpenApi();
    }
}