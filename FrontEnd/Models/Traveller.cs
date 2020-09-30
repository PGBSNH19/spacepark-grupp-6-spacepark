using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkWebApp.Models
{
    public class Traveller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Spaceship> Spaceships { get; set; }
    }
}
