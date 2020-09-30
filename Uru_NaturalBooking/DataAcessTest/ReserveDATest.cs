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
    public class ReserveDATest
    {
        TouristSpot touristSpot;
        Lodging lodging;
        CategoryTouristSpot categoryTouristSpot;
        Category aCategory;
        Reserve reserve;
        SearchOfLodging search; 

        [TestInitialize]
        public void SetUp()
        {
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

            touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Maldonado",
                Description = "Departamento donde la naturaleza y la tranquilidad desborda.",
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }
            };

            categoryTouristSpot.TouristSpot = touristSpot;
            categoryTouristSpot.TouristSpotId = touristSpot.Id;

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Las Cumbres",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150,
                TouristSpot = touristSpot
            };

            search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 2, 2, 1 }
            };

            reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                PhoneNumberOfContact = 29082733, 
                DescriptionForGuest= "Un lugar ideal para descansar", 
                LodgingOfReserve= lodging, 
                CheckIn= search.CheckIn, 
                CheckOut= search.CheckOut, 
                QuantityOfAdult= search.QuantityOfGuest[0], 
                QuantityOfChild= search.QuantityOfGuest[1], 
                QuantityOfBaby= search.QuantityOfGuest[2], 
                StateOfReserve= Reserve.ReserveState.Creada
            };

            
        }

        [TestMethod]
       public void TestAddReserveOK()
       {
            ContextObl context= ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            reserveRepository.Save();

            List<Reserve> listOfReserves = reserveRepository.GetAll().ToList();

            Assert.AreEqual(reserve, listOfReserves[0]); 
        }

        [TestMethod]
        public void TestGetReserveOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            reserveRepository.Save();
            Reserve reserveOfDb = reserveRepository.Get(reserve.Id);

            Assert.AreEqual(reserve, reserveOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void GetReserveDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            Reserve reserveOfDb = reserveRepository.Get(reserve.Id);
        }

        [TestMethod]
        public void TestRemoveReserveOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            reserveRepository.Save();
            List<Reserve> listOfReservefOfDbBeforeRemove = reserveRepository.GetAll().ToList();
            reserveRepository.Remove(reserve);
            reserveRepository.Save();

            List<Reserve> listOfReserveOfDbAfterRemove = reserveRepository.GetAll().ToList();

            CollectionAssert.AreNotEqual(listOfReservefOfDbBeforeRemove, listOfReserveOfDbAfterRemove);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveReserveDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Remove(reserve);
            reserveRepository.Save();
        }

        [TestMethod]
        public void TestUpdateReserveOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            reserveRepository.Save();

            reserve.Name = "Martin Gutman";

            reserveRepository.Update(reserve);
            reserveRepository.Save();

            List<Reserve> listOfReserve = reserveRepository.GetAll().ToList();

            Assert.AreEqual(reserve, listOfReserve[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateReserveInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Update(reserve);
            reserveRepository.Save();
        }

        [TestMethod]
        public void TestGetAllLodgingsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            SearchOfLodging searchForFamily = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 10, 25),
                CheckOut = new DateTime(2020, 12, 10),
                QuantityOfGuest = new int[3] { 2, 0, 1 }
            };

            Reserve reserveForAFamily = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                Email = "rgskins@gmail.com",
                PhoneNumberOfContact = 24006478,
                DescriptionForGuest = "Un lugar para pasar el rato con la familia, ideal para tomar un relax",
                LodgingOfReserve = lodging,
                CheckIn = search.CheckIn,
                CheckOut = search.CheckOut,
                QuantityOfAdult = searchForFamily.QuantityOfGuest[0],
                QuantityOfChild = searchForFamily.QuantityOfGuest[1],
                QuantityOfBaby = searchForFamily.QuantityOfGuest[2],
                StateOfReserve = Reserve.ReserveState.Creada
            };

            reserveRepository.Add(reserve);
            reserveRepository.Add(reserveForAFamily);
            reserveRepository.Save();

            List<Reserve> listWithOriginalsReserves = new List<Reserve>();
            listWithOriginalsReserves.Add(reserve);
            listWithOriginalsReserves.Add(reserveForAFamily);

            List<Reserve> listOfReserveOfDb = reserveRepository.GetAll().ToList();

            CollectionAssert.AreEqual(listWithOriginalsReserves, listOfReserveOfDb);
        }

    }
}
