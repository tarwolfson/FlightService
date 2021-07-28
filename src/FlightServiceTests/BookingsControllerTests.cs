using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using FlightAPI.Contracts;
using FlightAPI.Controllers;
using FlightAPI.MappingProfiles;
using FlightAPI.Models;
using FlightAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace FlightServiceTests
{
    public class BookingsControllerTests
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IFlightRepository _flightRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IMapper _mapper;
        private readonly IPassengerRepository _passengerRepo;

        public BookingsControllerTests()
        {
            _flightRepo = A.Fake<IFlightRepository>();
            _bookingRepo = A.Fake<IBookingRepository>();
            _passengerRepo = A.Fake<IPassengerRepository>();
            _logger = A.Fake<ILogger<BookingsController>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookingMappings());
                cfg.AddProfile(new FlightMappings());
            });
            _mapper = config.CreateMapper();
        }
        
        [Fact]
        public async Task Bookings_Get_Bookings_Returns_Correct_Number_Of_Items()
        {
            // Arrange
            const int bookingsCount = 5;
            var fakeId = A.Dummy<int>();
            var fakeBookings = A.CollectionOfDummy<Booking>(bookingsCount).AsEnumerable();
            A.CallTo(() => _bookingRepo.GetFlightBookings(fakeId)).Returns(Task.FromResult(fakeBookings));
            var controller = new BookingsController(_logger, _bookingRepo, _passengerRepo, _flightRepo, _mapper);
            
            // Act
            var actionResult = await controller.GetBookings(fakeId);

            // Assert
            var result = actionResult as OkObjectResult;
            var bookings = result.Value as IEnumerable<Booking>;
            Assert.Equal(bookingsCount, bookings.Count());
        }
        
        [Fact]
        public async Task Bookings_Post_With_Null_Dto_Returns_BadRequest()
        {
            // Arrange
            var fakeId = A.Dummy<int>();
            var controller = new BookingsController(_logger, _bookingRepo, _passengerRepo, _flightRepo, _mapper);

            // Act
            var actionResult = await controller.Post(fakeId, null);
            
            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
        
        [Fact]
        public async Task Bookings_Post_With_Bad_Id_Returns_NotFound()
        {
            // Arrange
            var fakeId = A.Dummy<int>();
            Flight emptyFlight = null;
            A.CallTo(() => _flightRepo.GetFlight(fakeId)).Returns(Task.FromResult(emptyFlight));
            var controller = new BookingsController(_logger, _bookingRepo, _passengerRepo, _flightRepo, _mapper);

            // Act
            var actionResult = await controller.Post(fakeId, null);
            
            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory]
        [InlineData(5,3)]
        [InlineData(6,4)]
        public async Task Bookings_Post_With_Bad_Dto_Returns_Conflict(int weight, int numberOfBags)
        {
            // Arrange
            var fakeId = A.Dummy<int>();
            var fakePassenger = A.Dummy<Passenger>();
            var fakeBookings = A.CollectionOfDummy<Booking>(5).AsEnumerable();
            var bookingDto = new BookingDto
            {
                BaggageWeight = weight,
                NumberOfBags = numberOfBags
            };
            var flight = new Flight
            {
                MaxBaggageWeightPerPassenger = 3,
                MaxBagsPerPassenger = 2,
                NumberOfSeats = 5
            };
            A.CallTo(() => _flightRepo.GetFlight(fakeId)).Returns(Task.FromResult(flight));
            A.CallTo(() => _passengerRepo.GetPassenger(A<int>._)).Returns(Task.FromResult(fakePassenger));
            A.CallTo(() => _bookingRepo.GetFlightBookings(fakeId)).Returns(Task.FromResult(fakeBookings));
            var controller = new BookingsController(_logger, _bookingRepo, _passengerRepo, _flightRepo, _mapper);
            
            // Act
            var actionResult = await controller.Post(fakeId, bookingDto);
            
            // Assert
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}