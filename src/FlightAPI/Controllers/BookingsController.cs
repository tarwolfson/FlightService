using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightAPI.Contracts;
using FlightAPI.Models;
using FlightAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightAPI.Controllers
{
    [ApiController]
    [Route("Flights/{flightId:int}/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public BookingsController(ILogger<BookingsController> logger,
            IBookingRepository bookingRepository,
            IPassengerRepository passengerRepository,
            IFlightRepository flightRepository,
            IMapper mapper)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
            _flightRepository = flightRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetBookings(int flightId)
        {
            try
            {
                var bookings = await _bookingRepository.GetFlightBookings(flightId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
            }
        }
        
        [HttpGet("{passengerId:int}")]
        public async Task<IActionResult> GetBooking(int flightId, int passengerId)
        {
            try
            {
                var booking = await _bookingRepository.GetBooking(flightId, passengerId);
                if (booking is null)
                {
                    return NotFound();
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(int flightId, BookingDto bookingDto)
        {
            try
            {
                if (bookingDto is null)
                {
                    return BadRequest();
                }

                var flight = await _flightRepository.GetFlight(flightId);
                var passenger = await _passengerRepository.GetPassenger(bookingDto.PassengerId);
                if (flight is null || passenger is null)
                {
                    return NotFound();
                }

                var flightBookings = await _bookingRepository.GetFlightBookings(flightId);
                var bookings = flightBookings.ToList();
                var currentBaggageWeight = bookings.Sum(f => f.BaggageWeight);
                if (bookings.Count >= flight.NumberOfSeats || bookingDto.NumberOfBags >= flight.MaxBagsPerPassenger ||
                    bookingDto.BaggageWeight + currentBaggageWeight > flight.MaxBaggageWeight ||
                    bookingDto.BaggageWeight > flight.MaxBaggageWeightPerPassenger)
                {
                    return Conflict();
                }

                var newBooking = _mapper.Map<Booking>(bookingDto);
                var booking = await _bookingRepository.AddFlightBooking(flightId, newBooking);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured.");
            }
        }

        [HttpPut("{passengerId:int}")]
        public async Task<IActionResult> Put(int flightId, int passengerId, BookingDto bookingDto)
        {
            try
            {
                if (bookingDto is null)
                {
                    return BadRequest();
                }

                var newBooking = _mapper.Map<Booking>(bookingDto, opt => 
                    opt.AfterMap((src, dest) => dest.FlightId = flightId));
                var bookings = await _bookingRepository.Update(flightId, passengerId, newBooking);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured.");
            }
        }

        [HttpDelete("{passengerId:int}")]
        public async Task<IActionResult> Delete(int flightId, int passengerId)
        {
            try
            {
                var bookings = await _bookingRepository.Delete(flightId, passengerId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured.");
            }
        }
    }
}