using DCXAirChallenge.Infrastructure.Services;

namespace DCXAirChallenge.Application.Services
{
    public class RouteService
    {
        private readonly List<Route> _routes;

        public RouteService(RouteLoaderService routeLoaderService)
        {
            // Cargar las rutas disponibles utilizando el servicio RouteLoaderService
            _routes = routeLoaderService.LoadRoutes("Data/routes.json");
        }

        public List<Route> GetRoutesByOriginAndDestination(string origin, string destination)
        {
            // Filtrar las rutas por origen y destino
            var filteredRoutes = _routes.Where(route => route.Origin == origin && route.Destination == destination).ToList();
            return filteredRoutes;
        }
    }
}
