using FlightAPI.Contracts;

namespace FlightAPI.Models
{
    public class Booking
    {
        public int PassengerId { get; set; }
        public int FlightId { get; set; }
        public int NumberOfBags { get; set; }
        public int BaggageWeight { get; set; }
    }
}