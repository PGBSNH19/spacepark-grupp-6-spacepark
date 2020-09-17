using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;

namespace spaceparkapi.Services
{
    public class ParkingspotRepository : Repository, IParkingspotRepository
    {
        private readonly SpaceContext _context;
        private readonly ILogger _logger;

        public ParkingspotRepository(SpaceContext context, ILogger logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
