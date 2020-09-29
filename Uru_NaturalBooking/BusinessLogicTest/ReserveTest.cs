using BusinessLogic;
using BusinessLogicException;
using DataAccessInterface;
using Domain;
using DomainException;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class ReserveTest
    {
        TouristSpot touristSpot;
        Lodging lodging;
        CategoryTouristSpot categoryTouristSpot;
        Category aCategory;

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
        }


        [TestMethod]
        public void CreateValidReserve()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 2, 2, 1 }
            }; 

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);

            reserveRepositoryMock.VerifyAll();
            Assert.AreEqual(lodging, resultOfCreateAReserve.LodgingOfReserve);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void CreateInvalidReserve()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>())).Throws(new ExceptionRepository());
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10), 
                QuantityOfGuest= new int[3] {2, 2, 1}
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutName()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 2, 2, 1 }
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);

        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutLastName()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "",
                Email = "joaquin.lamela@hotmail.com"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 2, 2, 1 }
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithInvalidDate()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 08, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 2, 2, 1 }
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutGuest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 0, 0, 0 }
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);
        }


        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithInvalidMail()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela"
            };

            SearchOfLodging search = new SearchOfLodging()
            {
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfGuest = new int[3] { 1, 0, 0 }
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id, search);
        }
    }
}
