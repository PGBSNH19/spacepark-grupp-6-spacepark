using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace spaceparkapi.Models
{
    public class Parkingspot : BaseEntity
    {
        [NotMapped]
        public readonly static int MaxLength = 500;


        public int? SpaceportId  { get; set; }
        [ForeignKey("SpaceportId")]
        public virtual Spaceport Spaceport { get; set; }


        public int? ParkedSpaceshipId  { get; set; }
        [ForeignKey("ParkedSpaceshipId")]
        public virtual Spaceship ParkedSpaceship { get; set; }


        public static bool SpaceshipFits(double length)
        {
            if (length > Parkingspot.MaxLength)
                return false;
            
            return true;
        }
    }
}
