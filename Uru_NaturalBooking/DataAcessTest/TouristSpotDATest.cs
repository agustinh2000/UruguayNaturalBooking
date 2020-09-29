using Castle.Core.Internal;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAcessTest
{
    [TestClass]
    public class TouristSpotDATest
    {
        Region aRegion;
        TouristSpot aTouristSpot;
        
        [TestInitialize]
        public void SetUp()
        {
            aRegion = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Este
            };

            aTouristSpot = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "Piriapolis",
                Description = "Un lugar unico",
                Region = aRegion
            };
        }

        [TestMethod]
        public void TestAddTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);
            regionRepo.Add(aRegion);
            touristSpotRepo.Add(aTouristSpot);
            List<TouristSpot> listOfTouristSpots = touristSpotRepo.GetAll().ToList();
            Assert.AreEqual(aTouristSpot, listOfTouristSpots[0]);
        }

        [TestMethod]
        public void TestGetTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);
            regionRepo.Add(aRegion);
            touristSpotRepo.Add(aTouristSpot);
            TouristSpot touristSpotOfDb = touristSpotRepo.Get(aTouristSpot.Id);
            Assert.AreEqual(aTouristSpot, touristSpotOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestGetTouristSpotBad()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            TouristSpot touristSpotOfDb = touristSpotRepo.Get(aTouristSpot.Id);
        }

        [TestMethod]
        public void TestRemoveTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            touristSpotRepo.Add(aTouristSpot);
            touristSpotRepo.Remove(aTouristSpot);
            List<TouristSpot> listOfTouristSpots = touristSpotRepo.GetAll().ToList();
            Assert.IsTrue(listOfTouristSpots.IsNullOrEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveTouristSpotInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            touristSpotRepo.Remove(aTouristSpot);
        }

        [TestMethod]
        public void TestUpdateTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            touristSpotRepo.Add(aTouristSpot);
            aTouristSpot.Name = "Piscinas";
            touristSpotRepo.Update(aTouristSpot);
            List<TouristSpot> listOfTouristSpots = touristSpotRepo.GetAll().ToList();
            Assert.AreEqual("Piscinas", listOfTouristSpots[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateTouristSpotInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            touristSpotRepo.Update(aTouristSpot);
            TouristSpot touristSpot = touristSpotRepo.Get(aTouristSpot.Id);
        }

        [TestMethod]
        public void TestGetAllTouristSpotsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<TouristSpot> touristSpotRepo = new BaseRepository<TouristSpot>(context);
            TouristSpot aTouristSpot2 = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "La Paloma",
                Description = "Un gran lugar",
                Region = aRegion
            };
            touristSpotRepo.Add(aTouristSpot);
            touristSpotRepo.Add(aTouristSpot2);
            List<TouristSpot> listTest = new List<TouristSpot>();
            listTest.Add(aTouristSpot);
            listTest.Add(aTouristSpot2);
            List<TouristSpot> listOfTouristSpots = touristSpotRepo.GetAll().ToList();
            CollectionAssert.AreEqual(listTest, listOfTouristSpots);
        }
    }
}
