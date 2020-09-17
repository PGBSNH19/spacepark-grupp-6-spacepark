using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using spaceparkapi.Services;
using spaceparkapi.Services.Interfaces;

namespace spaceparkapi.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ParkingspotController : ControllerBase
    {
        private readonly IParkingspotRepository _parkingspotRepository;
        public ParkingspotController(IParkingspotRepository parkingspotRepository)
        {
            _parkingspotRepository = parkingspotRepository;
        }
        
    }
}
