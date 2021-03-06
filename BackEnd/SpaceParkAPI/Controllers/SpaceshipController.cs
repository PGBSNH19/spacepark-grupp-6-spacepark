﻿using AutoMapper;
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
    public class SpaceshipController : ControllerBase
    {
        private readonly ISpaceshipRepository _spaceshipRepository;
        private readonly IMapper _mapper;

        public SpaceshipController(ISpaceshipRepository repository, IMapper mapper)
        {
            _spaceshipRepository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all spaceships.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<SpaceshipDto[]>> GetSpaceships()
        {
            try
            {
                var results = await _spaceshipRepository.GetAll<Spaceship>();
                var spaceshipResult = _mapper.Map<SpaceshipDto[]>(results);
                if (results == null)
                {
                    return NotFound($"Could not find any spaceship.");
                }
                return Ok(spaceshipResult);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}