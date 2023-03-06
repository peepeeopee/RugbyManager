using Microsoft.OpenApi.Models;
using RugbyManager.Application;
using RugbyManager.Infrastructure;
using RugbyManager.WebApi;
using RugbyManager.WebApi.Authentication;
using RugbyManager.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.AddSecurityDefinition("ApiKey",
        new OpenApiSecurityScheme
        {
            Description = "API Key used to access API",
            Type = SecuritySchemeType.ApiKey,
            Name = AuthenticationConstants.ApiKeyHeaderName,
            In = ParameterLocation.Header,
            Scheme = "ApiKeyScheme"
        });
    cfg.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Id = "ApiKey",
                        Type = ReferenceType.SecurityScheme
                    },
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
});
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

app.AddTeamEndPoints();
app.AddPlayerEndPoints();
app.AddStadiumEndpoints();
app.AddTransferEndpoints();

app.UseRateLimiter();

app.Run();