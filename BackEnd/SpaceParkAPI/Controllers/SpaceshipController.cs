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
    public class SpaceshipController : ControllerBase
    {
        private readonly ISpaceshipRepository _spaceshipRepository;
        private readonly IMapper _mapper;
        public SpaceshipController(ISpaceshipRepository repository, IMapper mapper)
        {
            _spaceshipRepository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<SpaceshipDto[]>> GetSpaceships()
        {
            try
            {
                var results = await _spaceshipRepository.GetAll<SpaceshipDto>("ParkingSpots");

                if (results == null)
                {
                    return NotFound($"Could not find any timetables");
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