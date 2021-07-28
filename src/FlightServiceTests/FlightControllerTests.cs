using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightAPI.Controllers;
using Xunit;
using FakeItEasy;
using FlightAPI.Contracts;
using FlightAPI.MappingProfiles;
using FlightAPI.Models;
using FlightAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightServiceTests
{
    public class FlightControllerTests
    {
        private readonly IFlightRepository _flightRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly ILogger<FlightsController> _logger;
        private readonly IMapper _mapper;
        
        public FlightControllerTests()
        {
            _flightRepo = A.Fake<IFlightRepository>();
            _bookingRepo = A.Fake<IBookingRepository>();
            _logger = A.Fake<ILogger<FlightsController>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookingMappings());
                cfg.AddProfile(new FlightMappings());
            });
            _mapper = config.CreateMapper();
        }
        
        [Fact]
        public async Task Flights_Post_Returns_All_Flights()
        {
            // Arrange
            const int flightCount = 5;
            var fakeFlights = A.CollectionOfDummy<Flight>(flightCount).AsEnumerable();
            var controller = new FlightsController(_logger, _flightRepo, _bookingRepo, _mapper);
            A.CallTo(() => _flightRepo.Get()).Returns(Task.FromResult(fakeFlights));

            // Act
            var actionResult = await controller.Get();
            
            // Assert
            var result = actionResult as OkObjectResult;
            var flightsResult = result.Value as IEnumerable<Flight>;
            Assert.Equal(flightCount, flightsResult.Count());
        }

        [Fact]
        public async Task Flights_Post_Adds_Flight()
        {
            // Arrange
            var fakeFlightDto = A.Dummy<FlightDto>();
            var fakeFlights = A.CollectionOfDummy<Flight>(1).AsEnumerable();
            A.CallTo(() => _flightRepo.Add(A<Flight>.Ignored)).Returns(Task.FromResult(fakeFlights));
            var controller = new FlightsController(_logger, _flightRepo, _bookingRepo, _mapper);
            
            // Act
            var actionResult = await controller.Post(fakeFlightDto);
            
            // Assert
            var result = actionResult as OkObjectResult;
            var flightsResult = result.Value as IEnumerable<Flight>;
            Assert.Single(flightsResult);
        }

        [Fact]
        public async Task Flights_Post_Null_Dto_Returns_BadRequest()
        {
            // Arrange
            var controller = new FlightsController(_logger, _flightRepo, _bookingRepo, _mapper);
            
            // Act
            var actionResult = await controller.Post(null);
            
            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task Flights_Delete_Returns_Correct_Number_Of_Flights()
        {
            // Arrange
            const int count = 0;
            var fakeId = A.Dummy<int>();
            var fakeFlight = A.Dummy<Flight>();
            var fakeEmpty = A.CollectionOfDummy<Flight>(count).AsEnumerable();
            A.CallTo(() => _flightRepo.GetFlight(fakeId)).Returns(Task.FromResult(fakeFlight));
            A.CallTo(() => _flightRepo.Delete(fakeId)).Returns(Task.FromResult(fakeEmpty));
            var controller = new FlightsController(_logger, _flightRepo, _bookingRepo, _mapper);
            
            // Act
            var actionResult = await controller.Delete(fakeId);
        
            // Assert
            var result = actionResult as OkObjectResult;
            var flights = result.Value as IEnumerable<Flight>;
            Assert.Empty(flights);
        }

        [Fact]
        public async Task Flights_Delete_Returns_NotFound()
        {
            // Arrange
            var fakeId = A.Dummy<int>();
            Flight emptyFlight = null;
            A.CallTo(() => _flightRepo.GetFlight(fakeId)).Returns(Task.FromResult(emptyFlight));
            var controller = new FlightsController(_logger, _flightRepo, _bookingRepo, _mapper);

            // Act
            var actionResult = await controller.Delete(fakeId);
            
            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
