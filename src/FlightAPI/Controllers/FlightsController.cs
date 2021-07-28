using System;
using System.Net;
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
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly IFlightRepository _flightRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public FlightsController(ILogger<FlightsController> logger,
            IFlightRepository flightRepository,
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _logger = logger;
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var flights = await _flightRepository.Get();
                return Ok(flights);
            }
            catch (Exception ex)
            {
                
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured.");
            }
        }

        [HttpGet("{flightId:int}")]
        public async Task<IActionResult> Get(int flightId)
        {
            try
            {
                var flight = await _flightRepository.GetFlight(flightId);
                return Ok(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> Post(FlightDto flightDto)
        {
            try
            {
                if (flightDto is null)
                {
                    return BadRequest();
                }
                
                var newFlight = _mapper.Map<Flight>(flightDto);
                var flights = await _flightRepository.Add(newFlight);
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
            }
        }

        [HttpPut("{flightId:int}")]
        public async Task<IActionResult> Put(int flightId, FlightDto flightDto)
        {
            try
            {
                if (flightDto is null)
                {
                    return BadRequest();
                }

                var newFlight = _mapper.Map<Flight>(flightDto);
                var flights=  await _flightRepository.Update(flightId, newFlight);
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
            }
        }

        [HttpDelete("{flightId:int}")]
        public async Task<IActionResult> Delete(int flightId)
        {
            try
            {
                var flight = await _flightRepository.GetFlight(flightId);

                if (flight is null)
                {
                    return NotFound();
                }
                
                var flights = await _flightRepository.Delete(flightId);
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Error}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured");
            }
        }
    }
}