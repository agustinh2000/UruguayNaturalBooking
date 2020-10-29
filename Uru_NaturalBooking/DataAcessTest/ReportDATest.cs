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
    public class ReportDATest
    {
        TouristSpot touristSpot;
        Lodging lodging;
        CategoryTouristSpot categoryTouristSpot;
        Category aCategory;
        Reserve reserve;
        Lodging lodging2;
        Lodging lodging3;
        Reserve reserve2; 

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

            lodging2 = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Conrad",
                QuantityOfStars = 5,
                Address = "La mansa de PDE",
                PricePerNight = 500,
                TouristSpot = touristSpot
            };

            lodging3 = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel San Ramon",
                QuantityOfStars = 2,
                Address = "En pleno Canelones",
                PricePerNight = 20,
                TouristSpot = touristSpot
            }; 

            reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                PhoneNumberOfContact = 29082733,
                DescriptionForGuest = "Un lugar ideal para descansar",
                LodgingOfReserve = lodging,
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1,
                StateOfReserve = Reserve.ReserveState.Creada
            };

            reserve2 = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Ricardo",
                LastName = "Hernandorena",
                Email = "ricardoHernandorena@hotmail.com",
                PhoneNumberOfContact = 29082733,
                DescriptionForGuest = "La va a pasar muy mal aca señor.",
                LodgingOfReserve = lodging3,
                CheckIn = new DateTime(2020, 05, 30),
                CheckOut = new DateTime(2020, 06, 5),
                QuantityOfAdult = 1,
                QuantityOfChild = 1,
                QuantityOfBaby = 1,
                StateOfReserve = Reserve.ReserveState.Pendiente_Pago
            };
        }

        [TestMethod]
        public void TestGenerateReportOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);
            ITouristSpotRepository touristSpotRepository = new TouristSpotRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context); 

            categoryRepository.Add(aCategory);
            touristSpotRepository.Add(touristSpot);
            lodgingRepository.Add(lodging);
            lodgingRepository.Add(lodging2); 
            reserveRepository.Add(reserve);
            
            DateTime checkInDate = new DateTime(2020, 05, 24);
            DateTime checkOutDate = new DateTime(2020, 06, 11);

            List<Lodging> listOfLodgingsWithReserve = lodgingRepository.GetLodgingsWithReserves(touristSpot.Id, checkInDate, checkOutDate); 
            
            Assert.IsTrue(listOfLodgingsWithReserve[0].Equals(lodging) 
                && listOfLodgingsWithReserve.Count==1 
                && listOfLodgingsWithReserve[0].QuantityOfReserveForThePeriod(checkInDate, checkOutDate)==1);
        }


        [TestMethod]
        public void TestGenerateReportOKWithMoreReserves()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);
            ITouristSpotRepository touristSpotRepository = new TouristSpotRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            categoryRepository.Add(aCategory);
            touristSpotRepository.Add(touristSpot);
            lodgingRepository.Add(lodging);
            lodgingRepository.Add(lodging2);
            reserve.LodgingOfReserve = lodging3; 
            reserveRepository.Add(reserve);
            reserveRepository.Add(reserve2); 

            DateTime checkInDate = new DateTime(2020, 05, 24);
            DateTime checkOutDate = new DateTime(2020, 06, 11);

            List<Lodging> listOfLodgingsWithReserve = lodgingRepository.GetLodgingsWithReserves(touristSpot.Id, checkInDate, checkOutDate);

            Assert.IsTrue(listOfLodgingsWithReserve.Contains(lodging3));
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGenerateReportWithoutLodgings()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);
            ITouristSpotRepository touristSpotRepository = new TouristSpotRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            categoryRepository.Add(aCategory);
            touristSpotRepository.Add(touristSpot);
            lodgingRepository.Add(lodging);

            DateTime checkInDate = new DateTime(2020, 05, 24);
            DateTime checkOutDate = new DateTime(2020, 06, 11);

            List<Lodging> listOfLodgingsWithReserve = lodgingRepository.GetLodgingsWithReserves(touristSpot.Id, checkInDate, checkOutDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGenerateReportWithBadTouristSpot()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            ILodgingRepository lodgingRepository = new LodgingRepository(context);
            ITouristSpotRepository touristSpotRepository = new TouristSpotRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            IRepository<Reserve> reserveRepository = new BaseRepository<Reserve>(context);

            categoryRepository.Add(aCategory);
            touristSpotRepository.Add(touristSpot);
            lodgingRepository.Add(lodging);
            lodgingRepository.Add(lodging2);
            reserveRepository.Add(reserve);

            DateTime checkInDate = new DateTime(2020, 05, 24);
            DateTime checkOutDate = new DateTime(2020, 06, 11);

            List<Lodging> listOfLodgingsWithReserve = lodgingRepository.GetLodgingsWithReserves(Guid.NewGuid(), checkInDate, checkOutDate);
        }
    }
}
