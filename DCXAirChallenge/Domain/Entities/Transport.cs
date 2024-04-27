namespace DCXAirChallenge.Domain.Entities
{
    public class Transport
    {
        public Transport(string flightCarrier, string flightNumber)
        {
            FlightCarrier = flightCarrier;
            FlightNumber = flightNumber;
        }

        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
    }
}
