using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightAPI.Models;

namespace FlightAPI.Repositories
{
    public interface IPassengerRepository
    {
        public Task<IEnumerable<Passenger>> GetPassengers();
        public Task<Passenger> GetPassenger(int id);
        
    }
}