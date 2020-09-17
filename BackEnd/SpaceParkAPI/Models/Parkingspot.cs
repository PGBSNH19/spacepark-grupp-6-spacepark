﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Parkingspot : BaseEntity
    {
        public Spaceport Spaceport { get; set; }
        public Spaceship ParkedSpaceship { get; set; }
    }
}
