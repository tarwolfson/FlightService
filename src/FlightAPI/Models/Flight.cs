using System.Collections;
using System.Collections.Generic;
using FlightAPI.Controllers;

namespace FlightAPI.Models
{
    public class Flight
    {
        public Flight()
        {
            
        }

        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public int MaxBaggageWeight { get; set; }
        public int MaxBagsPerPassenger { get; set; }
        public int MaxBaggageWeightPerPassenger { get; set; }
    }
}