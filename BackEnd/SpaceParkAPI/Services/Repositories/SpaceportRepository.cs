using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;
using System.Collections.Generic;
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

            return spaceport.ParkingSpots.FirstOrDefault(x => x.ParkedSpaceship == null && Parkingspot.SpaceshipFits(spaceship.Length));
        }

        public async Task<IList<Parkingspot>> GetTravellerParkingspots(int travellerId)
        {
            _logger.LogInformation($"Fetching all parkingspots occupied by the traveller with id {travellerId}");

            var parkingspots = await _context.Parkingspot
                    .Where(parkingspot => parkingspot.ParkedSpaceship.Traveller.Id == travellerId)
                    .Include(parkingspot => parkingspot.ParkedSpaceship)
                    .ToListAsync();
                    
            return parkingspots;
        }
    }
}