using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;

namespace spaceparkapi.Services
{
    public class ParkingspotRepository : IParkingspotRepository
    {
        private readonly SpaceContext _context;
        private readonly ILogger _logger;

        public ParkingspotRepository(SpaceContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
