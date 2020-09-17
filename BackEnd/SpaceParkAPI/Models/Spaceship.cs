using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.Models
{
    public class Spaceship : BaseEntity
    {
        public double Length { get; set; }
        public Traveller Traveller { get; set; }
    }
}
