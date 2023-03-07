using Microsoft.OpenApi.Models;
using RugbyManager.Application;
using RugbyManager.Infrastructure;
using RugbyManager.Infrastructure.DataPersistence;
using RugbyManager.WebApi;
using RugbyManager.WebApi.Authentication;
using RugbyManager.WebApi.Endpoints;
using Swashbuckle.AspNetCore.SwaggerUI;

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
    app.UseSwaggerUI(c =>
    {
        //Show more of the model by default
        c.DefaultModelExpandDepth(2);

        //Close all of the major nodes
        c.DocExpansion(DocExpansion.None);

        //Show the example by default
        c.DefaultModelRendering(ModelRendering.Example);

        //Turn on Try it by default
        c.EnableTryItOutByDefault();

        //Performance Requirement - sorry. Highlighting kills javascript rendering on big json
        c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    });
    
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.AddTeamEndPoints();
app.AddPlayerEndPoints();
app.AddStadiumEndpoints();
app.AddTransferEndpoints();

app.UseRateLimiter();

app.Run();