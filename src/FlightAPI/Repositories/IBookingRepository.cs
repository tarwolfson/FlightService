using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightAPI.Models;

namespace FlightAPI.Repositories
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetFlightBookings(int flightId);
        public Task<IEnumerable<Booking>> GetPassengerBookings(int passengerId);
        public Task<Booking> GetBooking(int flightId, int passengerId);
        public Task<Booking> AddFlightBooking(int flightId, Booking booking);
        public Task RemoveFlightBooking(int flightId, int passengerId);
        public Task<IEnumerable<Booking>> GetAllBookings();
        public Task<IEnumerable<Booking>> Delete(int flightId, int passengerId);
        public Task<IEnumerable<Booking>> Update(int flightId, int passengerId, Booking newBooking);
    }
}