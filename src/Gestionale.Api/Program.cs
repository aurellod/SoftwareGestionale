using Gestionale.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database: la stringa arriva dai segreti in sviluppo, dalle variabili d'ambiente in produzione
var connectionString = builder.Configuration.GetConnectionString("Gestionale")
    ?? throw new InvalidOperationException("Stringa di connessione 'Gestionale' mancante.");

builder.Services.AddDbContext<GestionaleDbContext>(options =>
    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention()); // IdCategoria -> id_categoria

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();