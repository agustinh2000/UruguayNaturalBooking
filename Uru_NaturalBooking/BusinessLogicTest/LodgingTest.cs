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
    public class LodgingTest
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
                Name = "Hotel Las Cumbres",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150
            };

        }

        [TestMethod]
        public void CreateValidLodging()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);

            lodgingRepositoryMock.VerifyAll();
            Assert.AreEqual(lodging, resultOfCreateALodging);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutName()
        {
            lodging.Name = ""; 

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void CreateInvalidLodgingWithErrorInAdd()
        {

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>())).Throws(new ExceptionRepository());
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutAddress()
        {
            lodging.Address = "";

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutQuantityOfStars()
        {
            lodging.QuantityOfStars = 0;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithMoreStars()
        {
            lodging.QuantityOfStars = 15;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithInvalidPrice()
        {
            lodging.PricePerNight = -10;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithExpensivePrice()
        {
            lodging.PricePerNight = 15000000;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            var resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        public void GetValidLodgingOk()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            var resultOfGetTheLodging = lodgingLogic.GetLodgingById(lodging.Id);
            Assert.AreEqual(lodging, resultOfGetTheLodging); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInGetValidLodging()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            var resultOfGetLodging = lodgingLogic.GetLodgingById(lodging.Id); 
        }


    }
}
