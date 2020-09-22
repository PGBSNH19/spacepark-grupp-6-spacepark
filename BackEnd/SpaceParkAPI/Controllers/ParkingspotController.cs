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
        
        [HttpGet("{id}", Name = "GetIdAsync")]
        public async Task<ActionResult<ParkingspotDto>> GetById(int id)
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

        [HttpPost]
        public async Task<ActionResult<Parkingspot>> ParkShip(Parkingspot parkingspot)
        {
            try
            {
                await _parkingspotRepository.Add(parkingspot);
                return Created($"/api/v1.0/Parkingspot/{parkingspot.Id}", parkingspot);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        
        [HttpDelete("{parkingId}")]
        public async Task<ActionResult> DeleteParkedShip(int parkingId)
        {
            try
            {
                await _parkingspotRepository.Delete<Parkingspot>(parkingId);
                return NoContent();
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}
