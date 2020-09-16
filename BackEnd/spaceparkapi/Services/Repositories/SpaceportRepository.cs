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
    public class SpaceportRepository : Repository, ISpaceport
    {
        private readonly ILogger<SpaceportRepository> _logger;

        public SpaceportRepository(SpaceContext context, ILogger<SpaceportRepository> logger) : base(context, logger)
        {
            _logger = logger;
        }
    }
}