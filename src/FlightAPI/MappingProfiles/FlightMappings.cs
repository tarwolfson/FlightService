using AutoMapper;
using FlightAPI.Contracts;
using FlightAPI.Models;

namespace FlightAPI.MappingProfiles
{
    public class FlightMappings : Profile
    {
        public FlightMappings()
        {
            CreateMap<Flight, FlightDto>().ReverseMap();
        }
    }
}