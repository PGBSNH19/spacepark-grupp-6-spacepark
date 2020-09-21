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
        public void SpaceshipFits_False()
        {
            var shipFits = Parkingspot.SpaceshipFits(999999999);
            Assert.IsFalse(shipFits);
        }

        [TestMethod]
        public void SpaceshipFits_True()
        {
            var shipFits = Parkingspot.SpaceshipFits(1);
            Assert.IsTrue(shipFits);
        }
    }
}
