using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Services.Repositories
{
    public class TravellerRepository : Repository, ITraveller
    {
        private readonly SpaceContext _context;
        private readonly ILogger<TravellerRepository> _logger;

        public TravellerRepository(SpaceContext context, ILogger<TravellerRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
