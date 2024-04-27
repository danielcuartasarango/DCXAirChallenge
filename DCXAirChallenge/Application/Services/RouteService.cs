using DCXAirChallenge.Domain.Entities;
using DCXAirChallenge.Infrastructure.Services;

namespace DCXAirChallenge.Application.Services
{
    public class RouteService(RouteLoaderService routeLoaderService)
    {
        private List<Routes> _routes = routeLoaderService.LoadRoutes("Data/markets.json");

        public List<Routes> GetRoutesByOriginAndDestination(string origin, string destination)
        {

            // Convertir los códigos de origen y destino a mayúsculas para realizar una comparación sin distinción entre mayúsculas y minúsculas
            origin = origin.ToUpper();
            destination = destination.ToUpper();

            // Filtrar las rutas por origen y destino
            var filteredRoutes = _routes.Where(route => route.Origin.ToUpper() == origin && route.Destination.ToUpper() == destination).ToList();

            // Si no se encontraron rutas, se podría intentar invertir el origen y el destino para ver si hay una ruta en la dirección opuesta
            if (filteredRoutes.Count == 0)
            {
                Console.WriteLine($"No se encontraron rutas directas de {origin} a {destination}. Intentando buscar rutas inversas...");

                // Intentar invertir los códigos de origen y destino
                filteredRoutes = _routes.Where(route => route.Origin.ToUpper() == destination && route.Destination.ToUpper() == origin).ToList();

                if (filteredRoutes.Count > 0)
                {
                    Console.WriteLine($"Se encontraron rutas inversas de {destination} a {origin}.");
                }
                else
                {
                    Console.WriteLine($"No se encontraron rutas inversas de {destination} a {origin}.");
                }
            }
            else
            {
                Console.WriteLine($"Se encontraron rutas directas de {origin} a {destination}.");
            }

            return filteredRoutes;
        }
    }
}
