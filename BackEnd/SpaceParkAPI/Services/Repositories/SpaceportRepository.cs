using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Parkingspot> GetAvailableParkingspot(int spaceportId, Spaceship spaceship, params string[] including)
        {
            _logger.LogInformation($"Fetching available parkingspot from the spaceport.");

            var spaceport = await Get<Spaceport>(spaceportId, including);

            //'500' needs to be replaced with Fredriks' new SpaceshipFits()-method in Spaceship-model.
            return spaceport.ParkingSpots.FirstOrDefault(x => x.ParkedSpaceship == null && spaceship.Length <= 500);
        }
    }
}