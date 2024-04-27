using DCXAirChallenge.Domain.Entities;
using DCXAirChallenge.Infrastructure.Services;

namespace DCXAirChallenge.Application.Services
{
    public class RouteService
    {
        private readonly List<Routes> _routes;

        public RouteService(RouteLoaderService routeLoaderService)
        {
            // Cargar las rutas disponibles utilizando el servicio RouteLoaderService
            _routes = routeLoaderService.LoadRoutes("Data/markets.json");
        }

        public List<Routes> GetRoutesByOriginAndDestination(string origin, string destination)
        {
            // Filtrar las rutas por origen y destino
            var filteredRoutes = _routes.Where(route => route.Origin == origin && route.Destination == destination).ToList();
            return filteredRoutes;
        }
    }
}
