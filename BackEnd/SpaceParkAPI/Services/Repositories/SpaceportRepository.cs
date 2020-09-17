using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Services.Repositories
{
    public class SpaceportRepository : Repository, ISpaceportRepository
    {
        private readonly SpaceContext _context;
        private readonly ILogger<SpaceportRepository> _logger;

        public SpaceportRepository(SpaceContext context, ILogger<SpaceportRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}