using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Spaceport : BaseEntity
    {
        public string Name { get; set; }
        public List<Parkingspot> ParkingSpots { get; set; }
    }
}
