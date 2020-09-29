using System;
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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingspotDto>> GetParkingspotById(int id)
        {
            try
            {
                var result = await _parkingspotRepository.GetParkingSpotInfoById(id);
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
        
        // Checkout a parked spaceship using only the parkingspot id. (spacePortId is optional)        /api/v1.0/Parkingspot/checkout?parkingId=1&spacePortId=500
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
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
        
        // Park a spaceship using the parkingspot id and spaceship id. (spacePortId is optional)     /api/v1.0/Parkingspot/park?parkingId=1&spaceshipId=1&spacePortId=500
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
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}
