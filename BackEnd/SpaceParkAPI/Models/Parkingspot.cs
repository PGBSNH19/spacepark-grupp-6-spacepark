using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Parkingspot : BaseEntity
    {
        public double MaxLength { get; set; }
        public Spaceship ParkedSpaceship { get; set; }
        public Spaceport Spaceport { get; set; }
    }
}
