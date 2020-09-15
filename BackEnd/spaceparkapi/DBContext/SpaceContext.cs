using Microsoft.EntityFrameworkCore;
using spaceparkapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceparkapi.DBContext
{
    public class SpaceContext : DbContext
    {
        public SpaceContext()
        {

        }

        public SpaceContext(DbContextOptions<SpaceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Movie
            builder.Entity<Spaceport>().ToTable("Spaceport");
            builder.Entity<Spaceport>().HasKey(p => p.Id);
            builder.Entity<Spaceport>().HasData(new
            {
                Id = 1,
                Name = "Test Spaceport"
            });
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Traveller> Travellers { get; set; }
        public virtual DbSet<Parkingspot> Parkingspot { get; set; }
        public virtual DbSet<Spaceport> Spaceport { get; set; }
        public virtual DbSet<Spaceship> Spaceship { get; set; }
    }
}
