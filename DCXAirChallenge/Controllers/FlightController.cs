using DCXAirChallenge.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly RouteService _routeService;

        public FlightController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes([FromQuery] string origin, [FromQuery] string destination)
        {
            Console.WriteLine(origin + destination);
            var routes = _routeService.GetRoutesByOriginAndDestination(origin, destination, 2);
            return Ok(routes);
        }
    }
}
