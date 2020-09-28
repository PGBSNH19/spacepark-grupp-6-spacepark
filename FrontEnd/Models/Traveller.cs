using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkWebApp.Models
{
    public class Traveller
    {
        public string Name { get; set; }
        public List<Spaceship> Spaceships { get; set; }
    }
}
