using System.Collections.Generic;
using System.Linq;
using DCXAirChallenge.Domain.Entities;
using DCXAirChallenge.Infrastructure.Services;

namespace DCXAirChallenge.Application.Services
{
    public class RouteService
    {
        private readonly List<Routes> _routes;

        // Constructor de la clase RouteService
        public RouteService(RouteLoaderService routeLoaderService)
        {
            // Cargar las rutas desde el servicio de carga de rutas
            _routes = routeLoaderService.LoadRoutes("Data/markets.json");
        }

        // Método para encontrar las rutas más cortas entre un origen y un destino
        public List<Routes[]> GetShortestRoutesByOriginAndDestination(string origin, string destination)
        {
            // Convertir el origen y el destino a mayúsculas y eliminar espacios en blanco al inicio y al final
            origin = origin.Trim().ToUpper();
            destination = destination.Trim().ToUpper();

            // Lista para almacenar las posibles rutas encontradas
            var possibleRoutes = new List<List<Routes>>();

            // Conjunto para almacenar los destinos visitados durante el proceso
            var visited = new HashSet<string>();

            // Cola para realizar el recorrido en anchura (BFS) a través de las rutas
            var queue = new Queue<List<Routes>>();

            // Inicializar la cola con rutas que comienzan en el origen
            foreach (var route in _routes.Where(r => r.Origin == origin))
            {
                queue.Enqueue(new List<Routes> { route });
                visited.Add(route.Destination);
            }

            // Realizar el recorrido en anchura a través de las rutas
            while (queue.Count > 0)
            {
                var currentRoute = queue.Dequeue(); // Obtener la siguiente ruta de la cola
                var lastDestination = currentRoute.Last().Destination; // Obtener el último destino en la ruta actual

                // Si el último destino coincide con el destino final, agregar la ruta a las posibles rutas
                if (lastDestination == destination)
                {
                    possibleRoutes.Add(currentRoute);
                    continue;
                }

                // Iterar sobre las rutas disponibles desde el último destino en la ruta actual
                foreach (var nextRoute in _routes.Where(r => r.Origin == lastDestination && !visited.Contains(r.Destination)))
                {
                    var newRoute = new List<Routes>(currentRoute); // Crear una nueva ruta basada en la ruta actual
                    newRoute.Add(nextRoute); // Agregar la próxima ruta a la nueva ruta
                    queue.Enqueue(newRoute); // Agregar la nueva ruta a la cola para su procesamiento posterior
                    visited.Add(nextRoute.Destination); // Marcar el destino de la próxima ruta como visitado
                }
            }

            // Encontrar las rutas más cortas entre el origen y el destino
            if (possibleRoutes.Any())
            {
                var shortestLength = possibleRoutes.Min(route => route.Count); // Obtener la longitud más corta entre las posibles rutas
                var shortestRoutes = possibleRoutes.Where(route => route.Count == shortestLength).ToList(); // Filtrar las posibles rutas para obtener las más cortas
                return shortestRoutes.Select(route => route.ToArray()).ToList(); // Convertir las rutas más cortas a matrices y devolverlas como una lista
            }
            else
            {
                return new List<Routes[]>(); // Si no se encontraron rutas posibles, devolver una lista vacía
            }
        }
    }
}
