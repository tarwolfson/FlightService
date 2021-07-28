namespace FlightAPI.Contracts
{
    public class FlightDto
    {
        public int NumberOfSeats { get; set; }
        public int MaxBaggageWeight { get; set; }
        public int MaxBagsPerPassenger { get; set; }
        public int MaxBaggageWeightPerPassenger { get; set; }
    }
}