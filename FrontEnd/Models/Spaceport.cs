using SpaceparkWebApp.Models;
using System.Collections.Generic;

namespace SpaceparkWebApp.Models
{
    public class Spaceport
    {
        public string Name { get; set; }
        public List<Parkingspot> ParkingSpots { get; set; }
    }
}
