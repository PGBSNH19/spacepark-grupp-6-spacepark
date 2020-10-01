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
        #region                             ParkingspotRepository  

        [TestMethod]
        public void GetParkingSpotInfoById_ValidData_ParkedSpaceship1OwnerEquals()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var spaceshipRepository = new ParkingspotRepository(spaceshipContextMock.Object, logger);

            // Act
            var parkingspotBySpaceshipId = spaceshipRepository.GetParkingSpotInfoById(1).Result;

            // Assert
            Assert.AreEqual("Testship One", parkingspotBySpaceshipId.ParkedSpaceship.Name);
        }

        [TestMethod]
        public void GetParkingSpotInfoById_ValidData_ParkedSpaceship2OwnerEquals()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var spaceshipRepository = new ParkingspotRepository(spaceshipContextMock.Object, logger);

            // Act
            var parkingspotBySpaceshipId = spaceshipRepository.GetParkingSpotInfoById(2).Result;

            // Assert
            Assert.AreEqual("Testship Two", parkingspotBySpaceshipId.ParkedSpaceship.Name);
        }

        [TestMethod]
        public void GetParkingSpotInfoById_InvalidParkingspotIdId_IsNull()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();
            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<ParkingspotRepository>>();
            var spaceshipRepository = new ParkingspotRepository(spaceshipContextMock.Object, logger);

            // Act
            var parkingspotById = spaceshipRepository.GetParkingSpotInfoById(99999).Result;

            // Assert
            Assert.IsNull(parkingspotById);
        }

        #endregion  

        #region                             SpaceportRepository

        [TestMethod]
        public void GetAvailableParkingspot_ValidDataZeroFree_IsNull()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();

            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            // Act
            var freeParkingspot = spaceportRepository.GetAvailableParkingspot(500, 200).Result;

            // Assert
            Assert.IsNull(freeParkingspot);
        }

        [TestMethod]
        public void GetAvailableParkingspot_ValidDataOneFree_ParkingspotIdIs30()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();

            parkingspots.Add(new Parkingspot(){Id = 30, ParkedSpaceship = null, SpaceportId = 500});

            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            // Act
            var freeParkingspot = spaceportRepository.GetAvailableParkingspot(500, 200).Result;

            // Assert
            Assert.AreEqual(30, freeParkingspot.Id);
        }

        [TestMethod]
        public void GetAvailableParkingspot_ValidDataTwoFree_ParkingspotIdIs30()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();

            parkingspots.Add(new Parkingspot(){Id = 30, ParkedSpaceship = null, SpaceportId = 500});
            parkingspots.Add(new Parkingspot(){Id = 31, ParkedSpaceship = null, SpaceportId = 500});

            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            // Act
            var freeParkingspot = spaceportRepository.GetAvailableParkingspot(500, 200).Result;

            // Assert
            Assert.AreEqual(30, freeParkingspot.Id);
        }

        [TestMethod]
        public void GetAllAvailableParkingspots_ValidData_ParkingspotCountEqualsTwo()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();

            parkingspots[3].ParkedSpaceship = null;
            parkingspots.Add(new Parkingspot(){Id = 25, ParkedSpaceship = null});

            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            // Act
            var freeParkingspots = spaceportRepository.GetAllAvailableParkingspots(200).Result;

            // Assert
            Assert.AreEqual(2, freeParkingspots.Count);
        }

        [TestMethod]
        public void GetAllAvailableParkingspots_ValidData_ParkingspotCountEqualsZero()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();

            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            // Act
            var freeParkingspots = spaceportRepository.GetAllAvailableParkingspots(200).Result;

            // Assert
            Assert.AreEqual(0, freeParkingspots.Count);
        }

        [TestMethod]
        public void GetAllAvailableParkingspots_ValidData_ParkingspotCountEqualsFour()
        {
            // Arrange
            IList<Parkingspot> parkingspots = GenerateParkingspotData();

            parkingspots.Add(new Parkingspot(){Id = 25, ParkedSpaceship = null});
            parkingspots.Add(new Parkingspot(){Id = 26, ParkedSpaceship = null});
            parkingspots.Add(new Parkingspot(){Id = 27, ParkedSpaceship = null});
            parkingspots.Add(new Parkingspot(){Id = 28, ParkedSpaceship = null});

            var spaceshipContextMock = new Mock<SpaceContext>();
            spaceshipContextMock.Setup(p => p.Parkingspot).ReturnsDbSet(parkingspots);

            var logger = Mock.Of<ILogger<SpaceportRepository>>();
            var spaceportRepository = new SpaceportRepository(spaceshipContextMock.Object, logger);

            // Act
            var freeParkingspots = spaceportRepository.GetAllAvailableParkingspots(200).Result;

            // Assert
            Assert.AreEqual(4, freeParkingspots.Count);
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

        #endregion


        #region                             Moq data to test against

        private IList<Spaceport> GenerateSpaceportData()
        {
            return new List<Spaceport>
            {
                new Spaceport
                {
                    Id = 500,
                    Name = "Test Spaceport",
                    ParkingSpots = GenerateParkingspotData().ToList()
                },
                new Spaceport
                {
                    Id = 600,
                    Name = "Tester Port Two",
                    ParkingSpots = GenerateParkingspotData().ToList()
                }
            };
        }

        public static IList<Parkingspot> GenerateParkingspotData()
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
                    ParkedSpaceshipId = 1,
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 1,
                        Length = 500,
                        Name = "Testship One",
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
                    ParkedSpaceshipId = 2,
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 2,
                        Length = 800,
                        Name = "Testship Two",
                        Traveller = new Traveller()
                        {
                            Id = 2,
                            Name = "Anakin Skywalker"
                        }
                    }
                },
                new Parkingspot
                {
                    Id = 3,
                    Spaceport = spaceport,
                    ParkedSpaceshipId = 1,
                    ParkedSpaceship = new Spaceship()
                    {
                        Id = 1,
                        Length = 500,
                        Name = "Testship One",
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

        #endregion
    
    }
}
