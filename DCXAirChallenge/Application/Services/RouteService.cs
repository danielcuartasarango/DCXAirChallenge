using DCXAirChallenge.Domain.Entities;
using DCXAirChallenge.Infrastructure.Services;
using System;
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

        public class RouteInformation
        {
            public List<Routes> Flights { get; set; }
            public double Price { get; set; } // Cambiar el tipo a double
            public string Origin { get; set; }
            public string Destination { get; set; }
        }

        // Método para encontrar las rutas posibles entre un origen y un destino
        public List<RouteInformation> GetRoutesByOriginAndDestination(string origin, string destination, string currency, string tripType)
        {
            // Convertir el origen y el destino a mayúsculas y eliminar espacios en blanco al inicio y al final
            origin = origin.Trim().ToUpper();
            destination = destination.Trim().ToUpper();

            // Lista para almacenar todas las rutas posibles encontradas
            var possibleRoutes = new List<RouteInformation>();

            // Cola para realizar el recorrido en anchura (BFS) a través de las rutas
            var queue = new Queue<RouteInformation>();

            // Inicializar la cola con la ruta de salida desde el origen
            var initialRoute = new RouteInformation
            {
                Flights = new List<Routes>(), // Inicializar la lista de vuelos
                Price = 0,
                Origin = origin,
                Destination = origin // Destino inicial es el origen
            };

            queue.Enqueue(initialRoute);

            // Realizar el recorrido en anchura a través de las rutas
            while (queue.Count > 0)
            {
                var currentRoute = queue.Dequeue(); // Obtener la siguiente ruta de la cola
                var lastDestination = currentRoute.Destination; // Obtener el último destino en la ruta actual

                // Si el último destino coincide con el destino final, agregar la ruta a las posibles rutas
                if (lastDestination == destination)
                {
                    // Si el tipo de viaje es de ida y vuelta, agregar la ruta de regreso a la ruta actual
                    if (tripType == "roundTrip")
                    {
                        // Crear la ruta de regreso invirtiendo las rutas
                        // Crear la ruta de regreso invirtiendo las rutas
                        var returnRoute = new RouteInformation
                        {
                            Flights = currentRoute.Flights.Select(flight => new Routes(
                                flight.Destination, // Intercambiar origen y destino
                                flight.Origin,      // Intercambiar origen y destino
                                flight.Price,
                                flight.Transport
                            )).Reverse().ToList(),
                            Price = currentRoute.Price,
                            Origin = destination, // El origen de la ruta de regreso es el destino de la ruta de ida
                            Destination = origin  // El destino de la ruta de regreso es el origen de la ruta de ida
                        };


                        // Concatenar la ruta de regreso a la ruta de ida
                        currentRoute.Flights.AddRange(returnRoute.Flights.Skip(1)); // Saltar el primer vuelo de la ruta de regreso para evitar duplicados
                        currentRoute.Price += returnRoute.Price;

                        possibleRoutes.Add(currentRoute); // Agregar la ruta completa a las posibles rutas
                    }
                    else
                    {
                        possibleRoutes.Add(currentRoute); // Agregar la ruta de ida a las posibles rutas
                    }

                    continue; // Continuar con la siguiente iteración del bucle
                }

                // Iterar sobre las rutas disponibles desde el último destino en la ruta actual
                foreach (var nextRoute in _routes.Where(r => r.Origin == lastDestination))
                {
                    // Evitar ciclos infinitos y la repetición de escalas
                    if (!currentRoute.Flights.Any(r => r.Destination == nextRoute.Destination) &&
                        !currentRoute.Flights.Any(r => r.Origin == nextRoute.Destination)) // Evitar volver a visitar un destino ya visitado
                    {
                        var newRoute = new RouteInformation
                        {
                            Flights = currentRoute.Flights.ToList(), // Copiar la lista de vuelos de la ruta actual
                            Price = currentRoute.Price + nextRoute.Price, // Sumar el precio de la nueva ruta al precio actual
                            Origin = currentRoute.Origin,
                            Destination = nextRoute.Destination
                        };

                        newRoute.Flights.Add(nextRoute); // Agregar la próxima ruta a la nueva ruta
                        queue.Enqueue(newRoute); // Agregar la nueva ruta a la cola para su procesamiento posterior
                    }
                }
            }

            return possibleRoutes;
        }
    }
}
