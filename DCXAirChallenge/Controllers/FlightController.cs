using DCXAirChallenge.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirChallenge.Controllers
{
    // Atributo [ApiController]: Indica que esta clase es un controlador de API y habilita varios comportamientos relacionados con la convención de nomenclatura y la validación de los modelos de entrada.
    // Atributo [Route("api/[controller]")]: Define la ruta base para las acciones de este controlador. El marcador de posición [controller] será reemplazado por el nombre del controlador (Flight en este caso).
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly RouteService _routeService; // Campo privado para almacenar una instancia de RouteService

        // Constructor del controlador FlightController
        public FlightController(RouteService routeService)
        {
            _routeService = routeService; // Inicializa el campo _routeService con la instancia proporcionada de RouteService
        }

        // Método de acción HTTP GET para obtener las rutas más cortas entre un origen y un destino
        [HttpGet("routes")] // Define la ruta relativa para acceder a este método de acción (api/flight/routes)
        public IActionResult GetRoutes(string origin, string destination)
        {
            // Llama al método GetShortestRoutesByOriginAndDestination de la instancia _routeService para obtener las rutas más cortas
            var routes = _routeService.GetRoutesByOriginAndDestination(origin, destination, 2);

            // Devuelve un resultado HTTP 200 (OK) con las rutas obtenidas como cuerpo de la respuesta
            return Ok(routes);
        }
    }
}
