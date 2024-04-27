using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DCXAirChallenge.Infrastructure.Services
{
    public class RouteLoaderService
    {
        public List<Route> LoadRoutes(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var routes = JsonConvert.DeserializeObject<List<Route>>(json);
            return routes;
        }
    }
}
