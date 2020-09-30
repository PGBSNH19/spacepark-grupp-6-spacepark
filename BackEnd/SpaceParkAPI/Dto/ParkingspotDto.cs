using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Dto
{
    public class ParkingspotDto
    {
        public int Id { get; set; }
        public int SpaceportId { get; set; }
        public int ParkedSpaceshipId { get; set; }
    }
}