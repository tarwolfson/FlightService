using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightAPI.Models;

namespace FlightAPI.Repositories
{
    public interface IFlightRepository
    {
        public Task<IEnumerable<Flight>> Get();
        public Task<Flight> GetFlight(int id);
        public Task<IEnumerable<Flight>> Add(Flight flight);
        public Task<IEnumerable<Flight>> Update(int id, Flight flight);
        public Task<IEnumerable<Flight>> Delete(int flightId);
    }
}