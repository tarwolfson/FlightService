using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightAPI.Models;

namespace FlightAPI.Repositories
{
    class BookingRepository : IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetFlightBookings(int flightId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetPassengerBookings(int passengerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Booking> GetBooking(int flightId, int passengerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Booking> AddFlightBooking(int flightId, Booking booking)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFlightBooking(int flightId, int passengerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllBookings()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Booking>> Delete(int flightId, int passengerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Booking>>Update(int flightId, int passengerId, Booking newBooking)
        {
            throw new System.NotImplementedException();
        }
    }
}