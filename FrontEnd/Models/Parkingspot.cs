using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceparkWebApp.Models
{
    public class Parkingspot
    {
        public int Id { get; set; }
        public int SpaceportId { get; set; }
        public int ParkedSpaceshipId { get; set; }
    }
}
