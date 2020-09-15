﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Spaceship : BaseEntity
    {
        public double Length { get; set; }
        public Traveller Owner { get; set; }
        public Parkingspot Parkingspot { get; set; }
    }
}
