namespace DCXAirChallenge.Domain.Entities
{
    public class Routes(string origin, string destination, double price, Transport transport )
    {
        public string Origin { get; set; } = origin;
        public string Destination { get; set; } = destination;
        public double Price { get; set; } = price;
        public Transport Transport { get; set; } = transport;

    }
}
