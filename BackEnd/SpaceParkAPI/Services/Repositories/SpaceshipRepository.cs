using System;
using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace spaceparkapi.Services
    {
        public class SpaceshipRepository : Repository, ISpaceship
        {
            private readonly ILogger<SpaceshipRepository> _logger;

            public SpaceshipRepository(SpaceContext context, ILogger<SpaceshipRepository> logger) : base(context, logger)
            {
                _logger = logger;
            }
        }
    }
   