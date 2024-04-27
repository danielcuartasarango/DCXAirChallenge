using DCXAirChallenge.Application.Services;
using DCXAirChallenge.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddSingleton<RouteLoaderService>();
builder.Services.AddSingleton<RouteService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Replace with your Angular project's origin
             .AllowAnyHeader()
             .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configura el pipeline de solicitud HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAngular"); // Add this line before MapControllers

app.UseAuthorization();

app.MapControllers();

app.Run();