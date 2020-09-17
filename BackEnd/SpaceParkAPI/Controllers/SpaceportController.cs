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

        public async Task<ActionResult<SpaceportDto[]>> GetSpaceports()
        {
            try
            {
                var results = await _spaceportRepository.GetAll<SpaceportDto>("ParkingSpots");

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