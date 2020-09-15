using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Traveller : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Spaceship> Spaceships { get; set; }
    }
}
