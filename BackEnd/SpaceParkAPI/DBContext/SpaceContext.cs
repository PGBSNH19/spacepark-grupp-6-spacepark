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
            builder.Entity<Spaceport>().ToTable("Spaceport");
            builder.Entity<Spaceport>().HasKey(p => p.Id);
            builder.Entity<Spaceport>().HasData(new
            {
                Id = 500,
                Name = "Test Spaceport"
            });

            builder.Entity<Traveller>().ToTable("Traveller");
            builder.Entity<Traveller>().HasKey(p => p.Id);
            builder.Entity<Traveller>().HasData(new
            {
                Id = 1,
                FirstName = "Luke",
                LastName = "Skywalker"
            },
            new
            {
                Id = 2,
                FirstName = "Darth",
                LastName = "Vader"
            });

            builder.Entity<Parkingspot>().ToTable("Parkingspot");
            builder.Entity<Parkingspot>().HasKey(p => p.Id);
            builder.Entity<Parkingspot>().HasData(new
            {
                Id = 1,
                SpaceportId = 500
            },
            new
            {
                Id = 2,
                SpaceportId = 500
            });

            builder.Entity<Spaceship>().ToTable("Spaceship");
            builder.Entity<Spaceship>().HasKey(p => p.Id);
            builder.Entity<Spaceship>().HasData(new
            {
                Id = 1,
                Length = 1337.20,
                TravellerId = 1

            },
            new
            {
                Id = 2,
                Length = 1010.10,
                TravellerId = 2

            });
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Traveller> Travellers { get; set; }
        public virtual DbSet<Parkingspot> Parkingspot { get; set; }
        public virtual DbSet<Spaceport> Spaceport { get; set; }
        public virtual DbSet<Spaceship> Spaceship { get; set; }
    }
}
