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
        public IActionResult GetRoutes(string origin, string destination)
        {
            var routes = _routeService.GetRoutesByOriginAndDestination(origin, destination);
            return Ok(routes);
        }
    }
}
