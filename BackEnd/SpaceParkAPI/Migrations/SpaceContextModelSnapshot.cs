﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using spaceparkapi.DBContext;

namespace spaceparkapi.Migrations
{
    [DbContext(typeof(SpaceContext))]
    partial class SpaceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("spaceparkapi.Models.Parkingspot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParkedSpaceshipId")
                        .HasColumnType("int");

                    b.Property<int?>("SpaceportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParkedSpaceshipId");

                    b.HasIndex("SpaceportId");

                    b.ToTable("Parkingspot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ParkedSpaceshipId = 1,
                            SpaceportId = 500
                        },
                        new
                        {
                            Id = 2,
                            ParkedSpaceshipId = 2,
                            SpaceportId = 500
                        });
                });

            modelBuilder.Entity("spaceparkapi.Models.Spaceport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Spaceport");

                    b.HasData(
                        new
                        {
                            Id = 500,
                            Name = "Test Spaceport"
                        });
                });

            modelBuilder.Entity("spaceparkapi.Models.Spaceship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<int?>("TravellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TravellerId");

                    b.ToTable("Spaceship");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Length = 1337.2,
                            TravellerId = 1
                        },
                        new
                        {
                            Id = 2,
                            Length = 1010.1,
                            TravellerId = 2
                        });
                });

            modelBuilder.Entity("spaceparkapi.Models.Traveller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Traveller");

                    b.HasData(
                        new
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
                });

            modelBuilder.Entity("spaceparkapi.Models.Parkingspot", b =>
                {
                    b.HasOne("spaceparkapi.Models.Spaceship", "ParkedSpaceship")
                        .WithMany()
                        .HasForeignKey("ParkedSpaceshipId");

                    b.HasOne("spaceparkapi.Models.Spaceport", "Spaceport")
                        .WithMany("ParkingSpots")
                        .HasForeignKey("SpaceportId");
                });

            modelBuilder.Entity("spaceparkapi.Models.Spaceship", b =>
                {
                    b.HasOne("spaceparkapi.Models.Traveller", "Traveller")
                        .WithMany("Spaceships")
                        .HasForeignKey("TravellerId");
                });
#pragma warning restore 612, 618
        }
    }
}
