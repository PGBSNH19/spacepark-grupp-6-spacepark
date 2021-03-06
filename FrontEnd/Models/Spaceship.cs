﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkWebApp.Models
{
    public class Spaceship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public string Parking { get; set; }
        public Traveller Traveller { get; set; }
    }
    public class SpaceshipResults
    {
        public List<Spaceship> results { get; set; }

    }
}
