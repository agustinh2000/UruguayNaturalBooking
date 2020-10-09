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

            reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                PhoneNumberOfContact = 29082733, 
                DescriptionForGuest= "Un lugar ideal para descansar", 
                LodgingOfReserve= lodging, 
                CheckIn= new DateTime(2020, 05, 25), 
                CheckOut= new DateTime(2020, 06, 10), 
                QuantityOfAdult= 2, 
                QuantityOfChild= 2, 
                QuantityOfBaby= 1, 
                StateOfReserve= Reserve.ReserveState.Creada
            };

            
        }

        [TestMethod]
       public void TestAddReserveOK()
       {
            ContextObl context= ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);

            List<Reserve> listOfReserves = reserveRepository.GetAll().ToList();

            Assert.AreEqual(reserve, listOfReserves[0]); 
        }

        [TestMethod]
        public void TestGetReserveOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            Reserve reserveOfDb = reserveRepository.Get(reserve.Id);

            Assert.AreEqual(reserve, reserveOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void GetReserveDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            Reserve reserveOfDb = reserveRepository.Get(reserve.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestRemoveReserveOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            reserveRepository.Remove(reserve);
            reserveRepository.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestRemoveReserveDoesntExist()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);
            reserveRepository.Remove(reserve);
        }

        [TestMethod]
        public void TestUpdateReserveOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Add(reserve);
            reserve.Name = "Martin Gutman";
            reserveRepository.Update(reserve);

            List<Reserve> listOfReserve = reserveRepository.GetAll().ToList();

            Assert.AreEqual(reserve, listOfReserve[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestUpdateReserveInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            reserveRepository.Update(reserve);
        }

        [TestMethod]
        public void TestGetAllLodgingsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            Reserve reserveForAFamily = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                Email = "rgskins@gmail.com",
                PhoneNumberOfContact = 24006478,
                DescriptionForGuest = "Un lugar para pasar el rato con la familia, ideal para tomar un relax",
                LodgingOfReserve = lodging,
                CheckIn = new DateTime(2020, 10, 25),
                CheckOut = new DateTime(2020, 12, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 0,
                QuantityOfBaby = 1,
                StateOfReserve = Reserve.ReserveState.Creada
            };

            reserveRepository.Add(reserve);
            reserveRepository.Add(reserveForAFamily);

            List<Reserve> listWithOriginalsReserves = new List<Reserve>();
            listWithOriginalsReserves.Add(reserve);
            listWithOriginalsReserves.Add(reserveForAFamily);

            List<Reserve> listOfReserveOfDb = reserveRepository.GetAll().ToList();

            CollectionAssert.AreEqual(listWithOriginalsReserves, listOfReserveOfDb);
        }

    }
}
