using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Services.Repositories
{
    public class ParkingspotRepository : Repository, IParkingspotRepository
    {
        private readonly SpaceContext _context;
        private readonly ILogger<ParkingspotRepository> _logger;

        public ParkingspotRepository(SpaceContext context, ILogger<ParkingspotRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Parkingspot> GetParkingSpotInfoById(int id)
        {
            _logger.LogInformation($"Getting parkingspot info by Id: {id}");

            var query = await _context.Parkingspot
                    .Where(parkingspot => parkingspot.Id == id)
                    .Include(parkedSpaceship => parkedSpaceship.ParkedSpaceship)
                    .Include(spaceport => spaceport.Spaceport)
                    .FirstOrDefaultAsync();

            return query;
        }
    }
}
