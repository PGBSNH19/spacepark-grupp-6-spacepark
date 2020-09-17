using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITraveller _travellerRepository;

        public TravellerController(ITraveller repository)
        {
            _travellerRepository = repository;
        }

        public async Task<ActionResult<Traveller[]>> GetTravellers()
        {
            try
            {
                var results = await _travellerRepository.GetAll<Traveller>();

                if (results == null)
                {
                    return NotFound($"Could not find any traveller");
                }
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}
