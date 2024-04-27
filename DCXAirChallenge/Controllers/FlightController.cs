using DCXAirChallenge.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DCXAirChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController(RouteService routeService) : ControllerBase
    {
        private readonly RouteService _routeService = routeService;

        [HttpGet("routes")]
        public IActionResult GetRoutes(string origin, string destination)
        {
            Console.WriteLine("-------");
            var routes = _routeService.GetShortestRoutesByOriginAndDestination(origin, destination);
            return Ok(routes);
        }
    }
}
