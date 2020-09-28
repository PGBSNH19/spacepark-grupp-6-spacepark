using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpaceparkWebApp.Models
{
    public class LogInUser
    {
        [Required]
        public string Name { get; set; }
    }
}