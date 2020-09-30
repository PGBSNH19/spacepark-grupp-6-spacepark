using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Dto
{
    public class SpaceportDto
    {
        public string Name { get; set; }
        public List<Parkingspot> ParkingSpots { get; set; }
    }
}