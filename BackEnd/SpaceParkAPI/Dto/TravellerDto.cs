using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Dto
{
    public class TravellerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SpaceshipDto> Spaceships { get; set; }
    }
}
