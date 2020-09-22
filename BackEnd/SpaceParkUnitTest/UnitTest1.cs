using Microsoft.VisualStudio.TestTools.UnitTesting;
using spaceparkapi.Services.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using spaceparkapi.DBContext;
using spaceparkapi.Models;
using Moq;


namespace SpaceParkUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SpaceshipFits_TooBig_False()
        {
            var shipFits = Parkingspot.SpaceshipFits(999999999);
            Assert.IsFalse(shipFits);
        }

        [TestMethod]
        public void SpaceshipFits_Smaller_True()
        {
            var shipFits = Parkingspot.SpaceshipFits(1);
            Assert.IsTrue(shipFits);
        }

        [TestMethod]
        public void SpaceshipFits_SameSize_True()
        {
            var shipFits = Parkingspot.SpaceshipFits(Parkingspot.MaxLength);
            Assert.IsTrue(shipFits);
        }

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
            Assert.AreEqual("Luke", parkingspotById.ParkedSpaceship.Traveller.FirstName);
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
            Assert.AreEqual("Anakin", parkingspotById.ParkedSpaceship.Traveller.FirstName);
        }

        private IList<Parkingspot> GenerateParkingspotData()
        {
            return new List<Parkingspot>
            {
                new Parkingspot
                {
                    Id = 1,
                    Spaceport = new Spaceport()
                    {
                        Id = 500,
                        Name = "Test Spaceport"
                    },
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 1,
                        Length = 500,
                        Traveller = new Traveller()
                        {
                            Id = 1,
                            FirstName = "Luke",
                            LastName = "Skywalker"
                        }
                    }
                },
                new Parkingspot
                {
                    Id = 2,
                    Spaceport = new Spaceport()
                    {
                        Id = 500,
                        Name = "Test Spaceport"
                    },
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 2,
                        Length = 800,
                        Traveller = new Traveller()
                        {
                            Id = 2,
                            FirstName = "Anakin",
                            LastName = "Skywalker"
                        }
                    }
                },

            };
        }
    }
}
