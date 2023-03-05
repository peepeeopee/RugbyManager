using RugbyManager.Application;
using RugbyManager.Infrastructure;
using RugbyManager.WebApi;
using RugbyManager.WebApi.Endpoints;

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

app.AddTeamEndPoints();
app.AddPlayerEndPoints();
app.AddStadiumEndpoints();
app.AddTransferEndpoints();

app.UseRateLimiter();

app.Run();