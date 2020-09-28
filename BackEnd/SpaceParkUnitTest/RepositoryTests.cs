using Microsoft.VisualStudio.TestTools.UnitTesting;
using spaceparkapi.Services.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using Moq;
using System.Linq;

namespace SpaceParkUnitTest
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void GetSpaceShipInfoById_ValidData_ParkedSpaceship1OwnerEquals()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var spaceshipRepository = new ParkingspotRepository(spaceshipContextMock.Object, logger);

            // Act
            var parkingspotById = spaceshipRepository.GetParkingSpotInfoById(1).Result;

            // Assert
            Assert.AreEqual("Luke Skywalker", parkingspotById.ParkedSpaceship.Traveller.Name);
        }

        [TestMethod]
        public void GetSpaceShipInfoById_ValidData_ParkedSpaceship2OwnerEquals()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var spaceshipRepository = new ParkingspotRepository(spaceshipContextMock.Object, logger);

            // Act
            var parkingspotById = spaceshipRepository.GetParkingSpotInfoById(2).Result;

            // Assert
            Assert.AreEqual("Anakin Skywalker", parkingspotById.ParkedSpaceship.Traveller.Name);
        }

        [TestMethod]
        public void GetTravellerParkingspots_ValidData_Count2()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            int travellerId = 2;
            int numberOfVehicles = 2;

            // Act
            var travellerParkingspots = spaceportRepository.GetTravellerParkingspots(travellerId).Result;

            // Assert
            Assert.AreEqual(numberOfVehicles, travellerParkingspots.Count);
        }

        [TestMethod]
        public void GetTravellerParkingspots_ValidData_Count1()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            int travellerId = 1;
            int numberOfVehicles = 1;

            // Act
            var travellerParkingspots = spaceportRepository.GetTravellerParkingspots(travellerId).Result;

            // Assert
            Assert.AreEqual(numberOfVehicles, travellerParkingspots.Count);
        }


        [TestMethod]
        public void GetTravellerParkingspots_InvalidTravellerId_Count0()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            int travellerId = 999999999;
            int numberOfVehicles = 0;

            // Act
            var travellerParkingspots = spaceportRepository.GetTravellerParkingspots(travellerId).Result;

            // Assert
            Assert.AreEqual(numberOfVehicles, travellerParkingspots.Count);
        }

        private IList<Spaceport> GenerateSpaceportData()
        {
            return new List<Spaceport>
            {
                new Spaceport
                {
                    Id = 500,
                    Name = "Test Spaceport",
                    ParkingSpots = GenerateParkingspotData().ToList()
                }
            };
        }

        private IList<Parkingspot> GenerateParkingspotData()
        {
            var spaceport = new Spaceport()
                {
                    Id = 500,
                    Name = "Test Spaceport"
                };

            return new List<Parkingspot>
            {
                new Parkingspot
                {
                    Id = 1,
                    Spaceport = spaceport,
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 1,
                        Length = 500,
                        Traveller = new Traveller()
                        {
                            Id = 1,
                            Name = "Luke Skywalker"
                        }
                    }
                },
                new Parkingspot
                {
                    Id = 2,
                    Spaceport = spaceport,
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 2,
                        Length = 800,
                        Traveller = new Traveller()
                        {
                            Id = 2,
                            Name = "Anakin Skywalker"
                        }
                    }
                },
                new Parkingspot
                {
                    Id = 2,
                    Spaceport = spaceport,
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 1,
                        Length = 500,
                        Traveller = new Traveller()
                        {
                            Id = 2,
                            Name = "Anakin Skywalker"
                        }
                    }
                },
                new Parkingspot
                {
                    Id = 24,
                    Spaceport = spaceport,
                    ParkedSpaceship = new Spaceship()
                    {
                        Traveller = new Traveller()
                    }
                }
            };
        }
    }
}
