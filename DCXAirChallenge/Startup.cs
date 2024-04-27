using DCXAirChallenge.Application.Services;
using DCXAirChallenge.Infrastructure.Services;

namespace DCXAirChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuración de servicios
            services.AddTransient<RouteLoaderService>();
            services.AddTransient<RouteService>();

            // Otros servicios y configuraciones...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuración de middleware y enrutamiento

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
