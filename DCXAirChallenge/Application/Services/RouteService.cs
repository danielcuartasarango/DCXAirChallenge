using DCXAirChallenge.Domain.Entities;
using DCXAirChallenge.Domain.Enums;
using DCXAirChallenge.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCXAirChallenge.Application.Services
{
    // Clase encargada de gestionar las rutas disponibles y encontrar rutas entre un origen y un destino
    public class RouteService
    {
        // Lista de rutas disponibles
        private readonly List<Routes> _routes;

        // Constructor de la clase RouteService que carga las rutas desde un archivo JSON
        public RouteService(RouteLoaderService routeLoaderService)
        {
            // Obtener las rutas desde el RouteLoaderService
            _routes = routeLoaderService.GetRoutes();
        }

        // Clase interna que representa la información de una ruta
        public class RouteInformation
        {
            public List<Routes> Flights { get; set; }  
            public double Price { get; set; }  
            public string Origin { get; set; } 
            public string Destination { get; set; }  
        }

        // Método principal para encontrar las rutas posibles entre un origen y un destino
        public List<RouteInformation> GetRoutesByOriginAndDestination(string origin, string destination, string currency, string tripType)
        {
            // Normalizar los inputs de origen y destino
            origin = NormalizeInput(origin);
            destination = NormalizeInput(destination);

            // Lista para almacenar todas las rutas posibles encontradas
            var possibleRoutes = new List<RouteInformation>();

            // Cola para realizar el recorrido en anchura a través de las rutas
            var queue = new Queue<RouteInformation>();

            // Inicializar la cola con la ruta de salida desde el origen
            var initialRoute = InitializeRoute(origin);

            queue.Enqueue(initialRoute);

            // Realizar el recorrido en anchura a través de las rutas
            while (queue.Count > 0)
            {
                var currentRoute = queue.Dequeue();
                var lastDestination = currentRoute.Destination;

                if (IsFinalDestination(lastDestination, destination))
                {
                    ProcessFinalDestination(currentRoute, tripType, currency, possibleRoutes);
                    continue;
                }

                ExploreNextRoutes(currentRoute, queue);
            }

            return possibleRoutes;
        }

        // Método para normalizar el input de origen o destino
        private string NormalizeInput(string input)
        {
            return input.Trim().ToUpper();
        }

        // Método para inicializar una nueva ruta con el origen dado
        private RouteInformation InitializeRoute(string origin)
        {
            return new RouteInformation
            {
                Flights = new List<Routes>(), 
                Price = 0,  
                Origin = origin,  
                Destination = origin  
            };
        }

        // Método para verificar si el último destino en la ruta es el destino final
        private bool IsFinalDestination(string lastDestination, string destination)
        {
            return lastDestination == destination;
        }

        // Método para procesar una ruta final y agregarla a las posibles rutas
        private void ProcessFinalDestination(RouteInformation currentRoute, string tripType, string currency, List<RouteInformation> possibleRoutes)
        {
            if (NormalizeInput(tripType) == "ROUNDTRIP")
            {
                AddReturnRoute(currentRoute);
            }

            AdjustCurrency(currentRoute, currency);
            possibleRoutes.Add(currentRoute);
        }

        // Método para agregar una ruta de regreso a la ruta actual
        private void AddReturnRoute(RouteInformation currentRoute)
        {
           
            var returnRoute = new RouteInformation
            {
                Flights = currentRoute.Flights.Select(flight => new Routes(
                    flight.Destination,  
                    flight.Origin,  
                    flight.Price,
                    flight.Transport
                )).Reverse().ToList(),
                Price = currentRoute.Price,
                Origin = currentRoute.Destination, 
                Destination = currentRoute.Origin  
            };

            // Concatenar la ruta de regreso a la ruta actual
            currentRoute.Flights.AddRange(returnRoute.Flights);  
            currentRoute.Price += returnRoute.Price;  
        }

        // Método para ajustar el precio de la ruta según la moneda
        private void AdjustCurrency(RouteInformation currentRoute, string currency)
        {
            if (NormalizeInput(currency) == "COP")
            {
                currentRoute.Price *= (int)Currency.COP;  
            }
        }

        // Método para explorar las rutas disponibles desde el último destino en la ruta actual
        private void ExploreNextRoutes(RouteInformation currentRoute, Queue<RouteInformation> queue)
        {
            var lastDestination = currentRoute.Destination;

            foreach (var nextRoute in _routes.Where(r => r.Origin == lastDestination))
            {
                if (!HasVisitedDestination(currentRoute, nextRoute.Destination))
                {
                    var newRoute = CreateNewRoute(currentRoute, nextRoute);
                    queue.Enqueue(newRoute);
                }
            }
        }

        // Método para verificar si ya se ha visitado un destino específico en la ruta actual
        private bool HasVisitedDestination(RouteInformation currentRoute, string destination)
        {
            return currentRoute.Flights.Any(r => r.Destination == destination) ||
                   currentRoute.Flights.Any(r => r.Origin == destination);
        }

        // Método para crear una nueva ruta a partir de la ruta actual y la siguiente ruta disponible
        private RouteInformation CreateNewRoute(RouteInformation currentRoute, Routes nextRoute)
        {
            var newRoute = new RouteInformation
            {
                Flights = currentRoute.Flights.ToList(),  
                Price = currentRoute.Price + nextRoute.Price,  
                Origin = currentRoute.Origin,  
                Destination = nextRoute.Destination  
            };

            newRoute.Flights.Add(nextRoute); 
            return newRoute;
        }
    }
}
