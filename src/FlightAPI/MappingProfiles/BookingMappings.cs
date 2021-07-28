using AutoMapper;
using FlightAPI.Contracts;
using FlightAPI.Models;

namespace FlightAPI.MappingProfiles
{
    public class BookingMappings : Profile
    {
        public BookingMappings()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
        }
    }
}