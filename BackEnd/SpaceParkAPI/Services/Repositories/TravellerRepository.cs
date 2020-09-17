using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Services.Repositories
{
    public class TravellerRepository : Repository, ITraveller
    {
        private readonly ILogger<TravellerRepository> _logger;

        public TravellerRepository(DBContext.SpaceContext context, ILogger<TravellerRepository> logger) : base(context, logger)
        {
            _logger = logger;
        }
    }
}
