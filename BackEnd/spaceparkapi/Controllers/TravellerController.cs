using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spaceparkapi.Dto;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TravellerController : ControllerBase
    {
        private readonly ITravellerRepository _travellerRepository;
        private readonly IMapper _mapper;

        public TravellerController(ITravellerRepository repository, IMapper mapper)
        {
            _travellerRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("Auth")]
        public async Task<ActionResult<TravellerDto>> AuthenticateTraveller([FromHeader] string name)
        {
            try
            {
                if (name == null)
                    return BadRequest("Please enter traveller name.");

                bool isFamous = await _travellerRepository.IsFamous(name);

                if (!isFamous)
                    return Unauthorized("Traveller " + name + " has not been in a Starwars movie.");

                Traveller traveller = _travellerRepository.GetTravellerByName(name).Result;

                if (traveller == null)
                {
                    traveller = _travellerRepository.RegisterTraveller(name).Result;
                }

                var newTraveller = _mapper.Map<TravellerDto>(traveller);

                return Ok(newTraveller);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Exception: {e.Message}");
            }
        }

        public async Task<ActionResult<TravellerDto[]>> GetTravellers()
        {
            try
            {
                var results = await _travellerRepository.GetAll<Traveller>();
                var mappedResult = _mapper.Map<TravellerDto[]>(results);

                if (results == null)
                {
                    return NotFound($"Could not find any traveller");
                }
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Exception: {e.Message}");
            }
        }
    }
}
