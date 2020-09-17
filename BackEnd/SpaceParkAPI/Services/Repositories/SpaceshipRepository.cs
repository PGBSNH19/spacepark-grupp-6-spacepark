using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Services.Repositories
{
    public class SpaceshipRepository : Repository, ISpaceshipRepository
    {
        private readonly SpaceContext _context;
        private readonly ILogger<SpaceshipRepository> _logger;

        public SpaceshipRepository(SpaceContext context, ILogger<SpaceshipRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
   