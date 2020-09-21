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
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}
