using System.Linq;
using System.Net;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using RugbyManager.Application;
using RugbyManager.Application.Common.Extensions;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models;
using RugbyManager.Application.Players.Commands;
using RugbyManager.Application.Stadiums.Commands;
using RugbyManager.Application.Teams.Commands;
using RugbyManager.Infrastructure;
using RugbyManager.WebApi;
using RugbyManager.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/team",
       async (IMediator mediator, IMapper mapper, AddTeamRequest request) =>
           await mediator.Send(request.Transform(mapper.Map<AddTeamCommand>))
   )
   .AddEndpointFilter<ValidationFilter<AddTeamRequest>>()
   .WithOpenApi();

app.MapPost("/player",
       async (IMediator Mediator, IMapper mapper, AddPlayerRequest request) =>
           await Mediator.Send(request.Transform(mapper.Map<AddPlayerCommand>))
   )
   .AddEndpointFilter<ValidationFilter<AddPlayerRequest>>()
   .WithOpenApi();


app.MapPost("/stadium",
       async (IMediator Mediator, IMapper mapper, AddStadiumRequest request) =>
           await Mediator.Send(request.Transform(mapper.Map<AddStadiumCommand>))
   )
   .AddEndpointFilter<ValidationFilter<AddStadiumRequest>>()
   .WithOpenApi();

app.UseRateLimiter();

app.Run();