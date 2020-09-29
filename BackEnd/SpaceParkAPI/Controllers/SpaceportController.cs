using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spaceparkapi.Dto;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class SpaceportController : ControllerBase
    {
        private readonly ISpaceportRepository _spaceportRepository;
        private readonly IMapper _mapper;

        public SpaceportController(ISpaceportRepository repository, IMapper mapper)
        {
            _spaceportRepository = repository;
            _mapper = mapper;
        }

        // Get first free parkingspot                        /api/v1.0/Spaceport/GetParkingSpot?spaceshipLength=200
        [HttpGet("GetParkingSpot")]
        public async Task<ActionResult<ParkingspotDto>> GetAvailableParkingspot(int spaceshipLength, int spaceportId = 500)
        {
            try
            {
                var result = await _spaceportRepository.GetAvailableParkingspot(spaceportId, spaceshipLength);
                var mappedResult = _mapper.Map<ParkingspotDto>(result);

                if (mappedResult == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(mappedResult);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }


        // Get all free parking spots        /api/v1.0/Spaceport/GetFreeParkingSpots?spaceshipLength=200
        [HttpGet("GetFreeParkingSpots")]
        public async Task<ActionResult<IList<ParkingspotDto>>> GetAllAvailableParkingspots(int spaceshipLength)
        {
            try
            {
                var result = await _spaceportRepository.GetAllAvailableParkingspots(spaceshipLength);
                var mappedResult = _mapper.Map<IList<ParkingspotDto>>(result);

                if (mappedResult == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(mappedResult);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        // Get all parked spaceships by traveller ID        /api/v1.0/Spaceport/traveller?id=1
        [HttpGet("traveller")]
        public async Task<ActionResult<IList<ParkingspotDto>>> GetTravellerParkingspots(int Id)
        {
            try
            {
                var result = await _spaceportRepository.GetTravellerParkingspots(Id);
                var mappedResult = _mapper.Map<IList<ParkingspotDto>>(result);

                if (mappedResult == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(mappedResult);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        public async Task<ActionResult<SpaceportDto[]>> GetSpaceports()
        {
            try
            {
                var results = await _spaceportRepository.GetAll<Spaceport>("ParkingSpots");
                var spaceportResult = _mapper.Map<SpaceportDto[]>(results);

                if (results == null)
                {
                    return NotFound($"Could not find any timetables");
                }
                return Ok(spaceportResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}