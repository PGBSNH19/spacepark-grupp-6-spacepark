﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ISpaceship _spaceshipRepository;

        public SpaceshipController(ISpaceship repository)
        {
            _spaceshipRepository = repository;
        }

        public async Task<ActionResult<Spaceship[]>> GetSpaceships()
        {
            try
            {
                var results = await _spaceshipRepository.GetAll<Spaceship>("ParkingSpots");

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