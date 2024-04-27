namespace DCXAirChallenge.Domain.Entities
{
    public class Routes
    {
        public Routes(string origin, string destination, double price, Transport transport)
        {
            Origin = origin;
            Destination = destination;
            Price = price;
            Transport = transport;
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public Transport Transport { get; set; }
    }
}
