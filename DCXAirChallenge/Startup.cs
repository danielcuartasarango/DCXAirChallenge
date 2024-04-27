using DCXAirChallenge.Application.Services;
using DCXAirChallenge.Infrastructure.Services;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("DDDD");
        // Configuración de servicios
        services.AddSingleton<RouteLoaderService>();
        services.AddSingleton<RouteService>();

        // Otros servicios y configuraciones...
    }

    public void Configure(IApplicationBuilder app)
    {
        // Configuración de middleware y enrutamiento
        Console.WriteLine("DDDD");
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
