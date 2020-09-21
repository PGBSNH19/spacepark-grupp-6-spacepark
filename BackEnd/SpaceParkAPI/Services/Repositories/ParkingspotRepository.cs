using System;
using System.Threading.Tasks;
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
        
    }
}
