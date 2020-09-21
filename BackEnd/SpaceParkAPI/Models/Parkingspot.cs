using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Parkingspot : BaseEntity
    {
        [NotMapped]
        public readonly static int MaxLength = 500;
        public Spaceport Spaceport { get; set; }
        public Spaceship ParkedSpaceship { get; set; }


        public static bool SpaceshipFits(double length)
        {
            if (length > Parkingspot.MaxLength)
                return false;
            
            return true;
        }
    }
}
