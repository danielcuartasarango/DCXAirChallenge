namespace DCXAirChallenge.Domain.Entities
{
    public class Journey
    {
        public Journey(List<Flight> flights, string origin, string destination, double price)
        {
            Flights = flights;
            Origin = origin;
            Destination = destination;
            Price = price;
        }

        public List<Flight> Flights { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
    }
}
