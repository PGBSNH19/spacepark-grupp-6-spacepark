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
    [Produces("application/json")]
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

        /// <summary>
        /// Gets all spaceports.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<SpaceportDto[]>> GetSpaceports()
        {
            try
            {
                var results = await _spaceportRepository.GetAll<Spaceport>("ParkingSpots");
                var spaceportResult = _mapper.Map<SpaceportDto[]>(results);

                if (results == null)
                {
                    return NotFound($"Could not find any spaceport");
                }
                return Ok(spaceportResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        /// <summary>
        /// Gets first free parkingspot.
        /// </summary>
        /// <param name="spaceshipLength"></param>
        /// <param name="spaceportId"></param>
        /// <returns></returns>
        //  /api/v1.0/Spaceport/GetParkingSpot?spaceshipLength=200
        [HttpGet("GetParkingSpot")]
        public async Task<ActionResult<ParkingspotDto>> GetAvailableParkingspot(int spaceshipLength, int spaceportId = 500)
        {
            try
            {
                var result = await _spaceportRepository.GetAvailableParkingspot(spaceportId, spaceshipLength);
                var mappedResult = _mapper.Map<ParkingspotDto>(result);

                if (mappedResult == null)
                {
                    return NotFound($"Could not find any parkingspot.");
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

        /// <summary>
        /// Gets all free parking spots.
        /// </summary>
        /// <param name="spaceshipLength"></param>
        /// <returns></returns>
        //  /api/v1.0/Spaceport/GetFreeParkingSpots?spaceshipLength=200
        [HttpGet("GetFreeParkingSpots")]
        public async Task<ActionResult<IList<ParkingspotDto>>> GetAllAvailableParkingspots(int spaceshipLength)
        {
            try
            {
                var result = await _spaceportRepository.GetAllAvailableParkingspots(spaceshipLength);
                var mappedResult = _mapper.Map<IList<ParkingspotDto>>(result);

                if (mappedResult == null)
                {
                    return NotFound($"Could not find any free parkingspot.");
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

        /// <summary>
        /// Gets all parked spaceships by traveller id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //  /api/v1.0/Spaceport/traveller?id=1
        [HttpGet("traveller")]
        public async Task<ActionResult<IList<ParkingspotDto>>> GetTravellerParkingspots(int Id)
        {
            try
            {
                var result = await _spaceportRepository.GetTravellerParkingspots(Id);
                var mappedResult = _mapper.Map<IList<ParkingspotDto>>(result);

                if (mappedResult == null)
                {
                    return NotFound($"Could not find any parked spaceship");
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
    }
}