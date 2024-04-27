
namespace DCXAirChallenge.Domain.Entities
{
    public class Flight
    {
        public Flight(Transport transport, string origin, string destination, double price)
        {
            Transport = transport;
            Origin = origin;
            Destination = destination;
            Price = price;
        }

        public Transport Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
    }
}
