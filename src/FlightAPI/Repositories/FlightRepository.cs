using System.Collections.Generic;
using System.Threading.Tasks;
using FlightAPI.Models;

namespace FlightAPI.Repositories
{
    class FlightRepository : IFlightRepository
    {
        public Task<IEnumerable<Flight>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<Flight> GetFlight(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Flight>> Add(Flight flight)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Flight>> Update(int id, Flight flight)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Flight>> Update(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Flight>> Delete(int flightId)
        {
            throw new System.NotImplementedException();
        }
    }
}