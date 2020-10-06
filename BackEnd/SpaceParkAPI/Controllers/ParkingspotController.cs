using System;
using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using spaceparkapi.Dto;
using spaceparkapi.Models;
using spaceparkapi.Services;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ParkingspotController : ControllerBase
    {
        private readonly IParkingspotRepository _parkingspotRepository;
        private readonly IMapper _mapper;

        public ParkingspotController(IParkingspotRepository parkingspotRepository, IMapper mapper)
        {
            _parkingspotRepository = parkingspotRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all parkingspots.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ParkingspotDto[]>> GetParkings()
        {
            try
            {
                var results = await _parkingspotRepository.GetAll<Parkingspot>("ParkedSpaceship");
                var mappedResults = _mapper.Map<ParkingspotDto[]>(results);

                if (mappedResults == null)
                {
                    return NotFound($"Could not find any parkingspot.");
                }
                else
                {
                    return Ok(mappedResults);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        /// <summary>
        /// Gets parkingspot by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingspotDto>> GetParkingspotById(int id)
        {
            try
            {
                var result = await _parkingspotRepository.GetParkingSpotInfoById(id);
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
        /// Checkout a parked spaceship using only the parkingspot id. (spacePortId is optional).
        /// </summary>
        /// <param name="parkingId"></param>
        /// <param name="spacePortId"></param>
        /// <returns></returns>
        // /api/v1.0/Parkingspot/checkout?parkingId=1&spacePortId=500
        [HttpPut("checkout")]
        public async Task<ActionResult> CheckoutParkedSpaceship(int parkingId, int spacePortId = 500)
        {
            try
            {
                var parkingspot = new Parkingspot()
                {
                    Id = parkingId,
                    SpaceportId = spacePortId,
                    ParkedSpaceship = null
                };
                await _parkingspotRepository.Update<Parkingspot>(parkingspot);
                return Ok(parkingspot);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        /// <summary>
        /// Park a spaceship using the parkingspot id and spaceship id. (spacePortId is optional).
        /// </summary>
        /// <param name="parkingId"></param>
        /// <param name="spaceshipId"></param>
        /// <param name="spacePortId"></param>
        /// <returns></returns>
        //  /api/v1.0/Parkingspot/park?parkingId=1&spaceshipId=1&spacePortId=500
        [HttpPut("park")]
        public async Task<ActionResult> ParkSpaceship(int parkingId, int spaceshipId, int spacePortId = 500)
        {
            try
            {
                var parkingspot = new Parkingspot()
                {
                    Id = parkingId,
                    SpaceportId = spacePortId,
                    ParkedSpaceshipId = spaceshipId
                };
                await _parkingspotRepository.Update<Parkingspot>(parkingspot);
                return Ok(parkingspot);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}