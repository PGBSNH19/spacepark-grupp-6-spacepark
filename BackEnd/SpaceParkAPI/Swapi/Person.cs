using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Swapi
{
    public class Person
    {
        public string name { get; set; }
        public List<string> starships { get; set; }
    }

    public class PersonResults
    {
        public List<Person> results { get; set; }
    }

    public class Starship
    {
        public string name { get; set; }
        public double length { get; set; }
    }

    public class StarShipResults
    {
        public List<Starship> results { get; set; }
    }
}
