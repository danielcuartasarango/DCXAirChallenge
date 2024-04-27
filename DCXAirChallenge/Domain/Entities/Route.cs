﻿namespace DCXAirChallenge.Domain.Entities
{
    public class Route
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public Transport Transport { get; set; }
    }
}
