using Castle.Core.Internal;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcessTest
{
    [TestClass]
    public class LodgingDATest
    {
        TouristSpot touristSpot; 
        Lodging lodging; 
        

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Maldonado",
                Description = "Departamento donde la naturaleza y la tranquilidad desborda."
            };

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(), 
                Name = "Hotel Las Cumbres",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150,
                TouristSpot = touristSpot,
            };
        }

        [TestMethod]
        public void TestAddLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            lodgingRepository.Add(lodging);
            lodgingRepository.Save();

            List<Lodging> listOfLodging = lodgingRepository.GetAll().ToList();

            Assert.AreEqual(lodging, listOfLodging[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestAddLodgingNullOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);
            lodgingRepository.Add(null);
        }

        [TestMethod]
        public void TestGetLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            lodgingRepository.Add(lodging);
            lodgingRepository.Save();
            Lodging lodgingOfDb = lodgingRepository.Get(lodging.Id);

            Assert.AreEqual(lodging, lodgingOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void GetLodgingDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            Lodging lodgingOfDb = lodgingRepository.Get(lodging.Id);
        }

        [TestMethod]
        public void TestRemoveLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            lodgingRepository.Add(lodging);
            lodgingRepository.Save();
            List<Lodging> listOfLodginfOfDbBeforeRemove = lodgingRepository.GetAll().ToList(); 
            lodgingRepository.Remove(lodging);
            lodgingRepository.Save();

            List<Lodging> listOfLodgingOfDbAfterRemove = lodgingRepository.GetAll().ToList();

            CollectionAssert.AreNotEqual(listOfLodginfOfDbBeforeRemove, listOfLodgingOfDbAfterRemove); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveLodgingDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            lodgingRepository.Remove(lodging);
            lodgingRepository.Save();
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveLodgingNull()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);
            lodgingRepository.Remove(null);
        }

        [TestMethod]
        public void TestUpdateLodgingOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            lodgingRepository.Add(lodging);
            lodgingRepository.Save();

            lodging.Name = "Hotel Enjoy Conrad";

            lodgingRepository.Update(lodging);
            lodgingRepository.Save();

            List<Lodging> listOfLodgings = lodgingRepository.GetAll().ToList();

            Assert.AreNotEqual("Hotel Las Cumbres", listOfLodgings[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateLodgingInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            lodgingRepository.Update(lodging);
            lodgingRepository.Save();
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateLodgingNullInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Lodging> lodgingRepository = new BaseRepository<Lodging>(context);
            lodgingRepository.Update(null);
        }

        [TestMethod]
        public void TestGetAllLodgingsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            Lodging lodgingOfConrad = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Enjoy Conrad",
                QuantityOfStars = 5,
                Address = "Parada 4 Playa Mansa, Rambla Claudio Williman",
                PricePerNight = 1500,
                TouristSpot = touristSpot,
            };

            lodgingRepository.Add(lodging);
            lodgingRepository.Add(lodgingOfConrad);
            lodgingRepository.Save();

            List<Lodging> listWithOriginalsLodgings = new List<Lodging>();
            listWithOriginalsLodgings.Add(lodging);
            listWithOriginalsLodgings.Add(lodgingOfConrad);

            List<Lodging> listOfLodgingOfDb = lodgingRepository.GetAll().ToList();

            CollectionAssert.AreEqual(listWithOriginalsLodgings, listOfLodgingOfDb);
        }

        [TestMethod]
        public void GetAvailableLodgingsByTouristSpotOk()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);

            Lodging lodgingOfConrad = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Enjoy Conrad",
                QuantityOfStars = 5,
                Address = "Parada 4 Playa Mansa, Rambla Claudio Williman",
                PricePerNight = 1500,
                TouristSpot = touristSpot,
            };

            lodgingRepository.Add(lodging);
            lodgingRepository.Add(lodgingOfConrad);
            lodgingRepository.Save();

            List<Lodging> listWithOriginalsLodgings = new List<Lodging>();
            listWithOriginalsLodgings.Add(lodging);
            listWithOriginalsLodgings.Add(lodgingOfConrad);

            List<Lodging> listOfLodgingOfDb = lodgingRepository.GetAvailableLodgingsByTouristSpot(touristSpot.Id).ToList();

            CollectionAssert.AreEqual(listWithOriginalsLodgings, listOfLodgingOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]

        public void GetAvailableLodgingsByTouristSpotNotFound()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);
            List<Lodging> listOfLodgingOfDb = lodgingRepository.GetAvailableLodgingsByTouristSpot(touristSpot.Id).ToList();
        }

    }
}
