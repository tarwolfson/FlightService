using System.ComponentModel.DataAnnotations;
using FlightAPI.Models;

namespace FlightAPI.Contracts
{
    public class BookingDto
    {
        [Required]
        public int PassengerId { get; set; }
        [Required]
        public int NumberOfBags { get; set; }
        [Required]
        public int BaggageWeight { get; set; }
    }
}