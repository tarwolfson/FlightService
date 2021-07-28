using System.Collections.Generic;
using System.Threading.Tasks;
using FlightAPI.Models;

namespace FlightAPI.Repositories
{
    class PassengerRepository : IPassengerRepository
    {
        public Task<IEnumerable<Passenger>> GetPassengers()
        {
            throw new System.NotImplementedException();
        }

        public Task<Passenger> GetPassenger(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}