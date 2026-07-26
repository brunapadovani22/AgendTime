using AgendTime.Application.Interfaces;
using AgendTime.Application.Services;
using AgendTime.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI nativo do .NET 10
builder.Services.AddOpenApi();

// Infrastructure (DbContext + Repositórios)
builder.Services.AddInfrastructure(builder.Configuration);

// Application (Serviços / Casos de uso)
builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();