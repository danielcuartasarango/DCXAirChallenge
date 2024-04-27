using DCXAirChallenge.Domain.Entities;
using DCXAirChallenge.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

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

        // Método para encontrar las rutas posibles entre un origen y un destino
        public List<Routes[]> GetRoutesByOriginAndDestination(string origin, string destination, int travelType)
        {
            // Convertir el origen y el destino a mayúsculas y eliminar espacios en blanco al inicio y al final
            origin = origin.Trim().ToUpper();
            destination = destination.Trim().ToUpper();

            // Lista para almacenar todas las rutas posibles encontradas
            var possibleRoutes = new List<List<Routes>>();

            // Cola para realizar el recorrido en anchura (BFS) a través de las rutas
            var queue = new Queue<List<Routes>>();

            // Inicializar la cola con rutas que comienzan en el origen
            foreach (var route in _routes.Where(r => r.Origin == origin))
            {
                queue.Enqueue(new List<Routes> { route });
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
                }

                // Iterar sobre las rutas disponibles desde el último destino en la ruta actual
                foreach (var nextRoute in _routes.Where(r => r.Origin == lastDestination))
                {
                    // Evitar ciclos infinitos permitiendo visitar destinos múltiples veces
                    if (!currentRoute.Any(r => r.Destination == nextRoute.Destination))
                    {
                        var newRoute = new List<Routes>(currentRoute); // Crear una nueva ruta basada en la ruta actual
                        newRoute.Add(nextRoute); // Agregar la próxima ruta a la nueva ruta
                        queue.Enqueue(newRoute); // Agregar la nueva ruta a la cola para su procesamiento posterior
                    }
                }
            }

            // Convertir todas las rutas posibles a matrices y devolverlas como una lista
            var allPossibleRoutes = possibleRoutes.Select(route => route.ToArray()).ToList();

            // Clasificar las rutas
            foreach (var route in allPossibleRoutes)
            {
                if (route.Length == 1 && travelType == 1)
                {
                    route[0].Category = "Ruta Solo Ida (Oneway)";
                }
                else if (route.Length == 1 && travelType == 2)
                {
                    route[0].Category = "Ruta Ida y Vuelta (Roundtrip)";
                }
                else if (route.Length > 1 && travelType == 1)
                {
                    route[0].Category = "Ruta con Múltiples Vuelos - Solo Ida";
                }
                else if (route.Length > 1 && travelType == 2)
                {
                    route[0].Category = "Ruta con Múltiples Vuelos - (Roundtrip)";
                }
            }

            return allPossibleRoutes;
        }
    }
    }
