using System.Collections.Generic;
using System.IO;
using DCXAirChallenge.Domain.Entities;
using Newtonsoft.Json;

namespace DCXAirChallenge.Infrastructure.Services
{
    public class RouteLoaderService
    {
        public List<Routes> LoadRoutes(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var routes = JsonConvert.DeserializeObject<List<Routes>>(json);
            return routes;
        }
    }
}
