using DCXAirChallenge.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DCXAirChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly RouteService _routeService;
        // Verificar que RouteService no sea nulo
        public FlightController(RouteService routeService)
        {
            _routeService = routeService ?? throw new ArgumentNullException(nameof(routeService)); 
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes([FromQuery] string origin, [FromQuery] string destination, [FromQuery] string currency, [FromQuery] string tripType)
        {
            try
            {
                // Devolver un resultado HTTP 200 OK con las rutas obtenidas
                var routes = _routeService.GetRoutesByOriginAndDestination(origin, destination, currency, tripType);
                return Ok(routes); 
            }
            catch (ArgumentException ex)
            {
                // Devolver un resultado HTTP 400 Bad Request con el mensaje de error
                return BadRequest($"Error en los parámetros de entrada: {ex.Message}"); 
            }
            catch (Exception ex)
            {
                // Devolver un resultado HTTP 500 Internal Server Error con el mensaje de error
                return StatusCode(500, $"Se ha producido un error interno: {ex.Message}"); 
            }
        }
    }
}

