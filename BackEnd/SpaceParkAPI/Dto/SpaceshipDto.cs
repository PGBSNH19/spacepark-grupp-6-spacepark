using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Dto
{
    public class SpaceshipDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        //public TravellerDto Owner { get; set; }
        //public ParkingspotDto Parkingspot { get; set; }
    }
}
