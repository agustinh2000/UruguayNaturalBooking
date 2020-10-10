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
        Category aCategory;
        CategoryTouristSpot categoryTouristSpot;

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

            aCategory = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa",
            };
            categoryTouristSpot = new CategoryTouristSpot()
            {
                Category = aCategory,
                CategoryId = aCategory.Id,
            };
        }

        [TestMethod]
        public void TestAddTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);
            regionRepo.Add(aRegion);
            touristSpotRepo.Add(aTouristSpot);
            List<TouristSpot> listOfTouristSpots = touristSpotRepo.GetAll().ToList();
            Assert.AreEqual(aTouristSpot, listOfTouristSpots[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestInvalidAddTouristSpot()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);
            regionRepo.Add(aRegion);
            touristSpotRepo.Add(null);
        }


        [TestMethod]
        public void TestGetTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);
            regionRepo.Add(aRegion);
            touristSpotRepo.Add(aTouristSpot);
            TouristSpot touristSpotOfDb = touristSpotRepo.Get(aTouristSpot.Id);
            Assert.AreEqual(aTouristSpot, touristSpotOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGetTouristSpotBad()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Get(aTouristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestRemoveTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Add(aTouristSpot);
            touristSpotRepo.Remove(aTouristSpot);
            touristSpotRepo.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestRemoveTouristSpotInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Remove(aTouristSpot);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestRemoveTouristSpotNullInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Remove(null);
        }

        [TestMethod]
        public void TestUpdateTouristSpotOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Add(aTouristSpot);
            aTouristSpot.Name = "Piscinas";
            touristSpotRepo.Update(aTouristSpot);
            List<TouristSpot> listOfTouristSpots = touristSpotRepo.GetAll().ToList();
            Assert.AreEqual("Piscinas", listOfTouristSpots[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestUpdateTouristSpotInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Update(aTouristSpot);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestUpdateTouristSpotNullInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Update(null);
        }

        [TestMethod]
        public void TestGetAllTouristSpotsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
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

        [TestMethod]
        public void TestGetTouristSpotsByRegionIdOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            TouristSpot aTouristSpot2 = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "La Paloma",
                Description = "Un gran lugar",
                Region = aRegion
            };
            touristSpotRepo.Add(aTouristSpot);
            touristSpotRepo.Add(aTouristSpot2);
            List<TouristSpot> listToCompare = new List<TouristSpot>();
            listToCompare.Add(aTouristSpot);
            listToCompare.Add(aTouristSpot2);
            List<TouristSpot> touristSpotsByRegion = touristSpotRepo.GetTouristSpotByRegion(aRegion.Id);
            CollectionAssert.AreEqual(listToCompare, touristSpotsByRegion);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestFailInGetTouristSpotsByRegion()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            List<TouristSpot> touristSpotsByRegion = touristSpotRepo.GetTouristSpotByRegion(aTouristSpot.Region.Id);
        }

        [TestMethod]
        public void TestGetTouristSpotsByCategoriesIdOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);

            TouristSpot aTouristSpot2 = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "La Paloma",
                Description = "Un gran lugar",
                Region = aRegion,
                ListOfCategories = new List<CategoryTouristSpot>() { }
            };
            categoryTouristSpot.TouristSpot = aTouristSpot2;
            categoryTouristSpot.TouristSpotId = aTouristSpot2.Id;
            aTouristSpot2.ListOfCategories.Add(categoryTouristSpot);
            touristSpotRepo.Add(aTouristSpot2);
            List<TouristSpot> listToCompare = new List<TouristSpot>() { aTouristSpot2 };
            List<Guid> listOfCategoriesIdToSearch = new List<Guid>() { categoryTouristSpot.CategoryId };

            List<TouristSpot> touristSpotsByCategories = touristSpotRepo.GetTouristSpotsByCategories(listOfCategoriesIdToSearch);
            CollectionAssert.AreEqual(listToCompare, touristSpotsByCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGetTouristSpotsByCategoriesIdInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            List<TouristSpot> touristSpotsByCategories = touristSpotRepo.GetTouristSpotsByCategories(null);
        }

        [TestMethod]
        public void TestGetTouristSpotsByCategoriesIdAndRegionIdOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);

            TouristSpot aTouristSpot2 = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "La Paloma",
                Description = "Un gran lugar",
                Region = aRegion,
                ListOfCategories = new List<CategoryTouristSpot>() { }
            };
            categoryTouristSpot.TouristSpot = aTouristSpot2;
            categoryTouristSpot.TouristSpotId = aTouristSpot2.Id;
            aTouristSpot2.ListOfCategories.Add(categoryTouristSpot);
            touristSpotRepo.Add(aTouristSpot2);
            List<TouristSpot> listToCompare = new List<TouristSpot>() { aTouristSpot2 };
            List<Guid> listOfCategoriesIdToSearch = new List<Guid>() { categoryTouristSpot.CategoryId };

            List<TouristSpot> touristSpotsByRegionAndCategories = touristSpotRepo.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesIdToSearch, aRegion.Id);
            CollectionAssert.AreEqual(listToCompare, touristSpotsByRegionAndCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGetTouristSpotsByCategoriesAndRegionIdInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            List<TouristSpot> touristSpotsByCategoriesAndRegion = touristSpotRepo.GetTouristSpotsByCategoriesAndRegion(null, aRegion.Id);
        }

        [TestMethod]
        public void TestGetTouristSpotByNameOk()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            touristSpotRepo.Add(aTouristSpot);
            TouristSpot touristSpotResult = touristSpotRepo.GetTouristSpotByName(aTouristSpot.Name);
            Assert.AreEqual(aTouristSpot, touristSpotResult);
        }
    }
}
