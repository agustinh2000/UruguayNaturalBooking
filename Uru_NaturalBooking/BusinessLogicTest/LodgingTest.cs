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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot}
            };

            categoryTouristSpot.TouristSpot = touristSpot;
            categoryTouristSpot.TouristSpotId = touristSpot.Id;

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(), 
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


        [TestMethod]
        public void GetLodgingsByTouristSpot()
        {
            TouristSpot touristSpotOfPuntaDelEste = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Donde el lujo y la naturaleza convergen, las mejores playas del Uruguay."
            }; 

            Lodging lodgingConrad = new Lodging()
            {
                Id = Guid.NewGuid(), 
                Name = "Hotel Enjoy Conrad",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 1500, 
                TouristSpot= touristSpotOfPuntaDelEste
            };

            lodging.TouristSpot = touristSpot; 

            List<Lodging> listOfLodgings = new List<Lodging>() { lodging, lodgingConrad }; 

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(listOfLodgings);
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            List<Lodging> resultOfSearchLodgingByTouristSpot = lodgingLogic.GetAvailableLodgingsByTouristSpot(touristSpot.Id);

            lodgingRepositoryMock.VerifyAll();
            Assert.AreEqual(lodging, resultOfSearchLodgingByTouristSpot[0]); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInGetLodgingByTouristSpot()
        {
            TouristSpot touristSpotOfPuntaDelEste = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Donde el lujo y la naturaleza convergen, las mejores playas del Uruguay."
            };

            Lodging lodgingConrad = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Enjoy Conrad",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 1500,
                TouristSpot = touristSpotOfPuntaDelEste
            };

            lodging.TouristSpot = touristSpot;

            List<Lodging> listOfLodgings = new List<Lodging>() { lodging, lodgingConrad };

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            List<Lodging> resultOfSearchLodgingByTouristSpot = lodgingLogic.GetAvailableLodgingsByTouristSpot(touristSpot.Id);
        }

        [TestMethod]
        public void UpdateValidLodging()
        {
            lodging.Name = "San Ramon Hotel";
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);
            lodgingRepositoryMock.Setup(m => m.Update(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            var resultOfUpdate = lodgingLogic.UpdateLodging(lodging);
            lodgingRepositoryMock.VerifyAll();
            Assert.IsTrue(resultOfUpdate.Name.Equals("San Ramon Hotel"));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInUpdateLodging()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            lodgingRepositoryMock.Setup(m => m.Update(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            var resultOfUpdate = lodgingLogic.UpdateLodging(lodging);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInUpdateNullLodging()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);
            lodgingRepositoryMock.Setup(m => m.Update(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            var resultOfUpdate = lodgingLogic.UpdateLodging(lodging);
        }

        [TestMethod]
        public void RemoveValidLodging()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);
            lodgingRepositoryMock.Setup(m => m.Remove(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Lodging>());
            lodgingRepositoryMock.Setup(m => m.Save());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            lodgingLogic.RemoveLodging(lodging.Id);
            List<Lodging> listOfLodging = lodgingLogic.GetAllLoadings();
            lodgingRepositoryMock.VerifyAll();
            Assert.IsTrue(listOfLodging.Count==0); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void RemoveInvalidLodging()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            lodgingRepositoryMock.Setup(m => m.Remove(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Lodging>());
            lodgingRepositoryMock.Setup(m => m.Save());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            lodgingLogic.RemoveLodging(lodging.Id);
        }

        [TestMethod]
        public void GetAllLodgings()
        {
            List<Lodging> lodgingsToReturn = new List<Lodging>() {lodging};

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(lodgingsToReturn);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            List<Lodging> lodgingsObteinedOfGetAll = lodgingLogic.GetAllLoadings();

            lodgingRepositoryMock.VerifyAll();

            CollectionAssert.AreEqual(lodgingsToReturn, lodgingsObteinedOfGetAll);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailAboutGetAllLodgings()
        {
            List<Lodging> lodgingsToReturn = new List<Lodging>() {lodging};
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());
            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            List<Lodging> loadgingsObteinedOfGetAll = lodgingLogic.GetAllLoadings();
        }
    }
}
