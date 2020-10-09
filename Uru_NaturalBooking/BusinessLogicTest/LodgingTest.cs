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
                Description = "Magnifico hospedaje",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150, 
                TouristSpot= touristSpot
            };

        }

        [TestMethod]
        public void CreateValidLodgingTestOk()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);

            Lodging lodgingToCompare = new Lodging()
            {
                Id = resultOfCreateALodging.Id,
                Name = resultOfCreateALodging.Name,
                QuantityOfStars = resultOfCreateALodging.QuantityOfStars,
                Address = resultOfCreateALodging.Address,
                Images = resultOfCreateALodging.Images,
                PricePerNight = resultOfCreateALodging.PricePerNight,
                IsAvailable = resultOfCreateALodging.IsAvailable,
                TouristSpot = resultOfCreateALodging.TouristSpot
            }; 

            lodgingRepositoryMock.VerifyAll();
            Assert.AreEqual(lodging, lodgingToCompare);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutNameTest()
        {
            lodging.Name = ""; 

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void CreateInvalidLodgingWithErrorInAddTest()
        {

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>())).Throws(new ExceptionRepository());
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutAddressTest()
        {
            lodging.Address = "";

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutDescriptionTest()
        {
            lodging.Description = "";

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithoutQuantityOfStarsTest()
        {
            lodging.QuantityOfStars = 0;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithMoreStarsTest()
        {
            lodging.QuantityOfStars = 15;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithInvalidPriceTest()
        {
            lodging.PricePerNight = -10;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(LodgingException))]
        public void CreateInvalidLodgingWithExpensivePriceTest()
        {
            lodging.PricePerNight = 15000000;

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot.Id);
        }

        [TestMethod]
        public void GetValidLodgingTestOk()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            Lodging resultOfGetTheLodging = lodgingLogic.GetLodgingById(lodging.Id);
            Assert.AreEqual(lodging, resultOfGetTheLodging); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInGetValidLodgingTest()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            Lodging resultOfGetLodging = lodgingLogic.GetLodgingById(lodging.Id); 
        }


        [TestMethod]
        public void GetLodgingsByTouristSpotTest()
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
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            List<Lodging> resultOfSearchLodgingByTouristSpot = lodgingLogic.GetAvailableLodgingsByTouristSpot(touristSpot.Id);

            lodgingRepositoryMock.VerifyAll();
            Assert.AreEqual(lodging, resultOfSearchLodgingByTouristSpot[0]); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInGetLodgingByTouristSpotTest()
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
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            List<Lodging> resultOfSearchLodgingByTouristSpot = lodgingLogic.GetAvailableLodgingsByTouristSpot(touristSpot.Id);
        }

        [TestMethod]
        public void UpdateValidLodgingTest()
        {
            lodging.Name = "San Ramon Hotel";
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);
            lodgingRepositoryMock.Setup(m => m.Update(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            Lodging resultOfUpdate = lodgingLogic.UpdateLodging(lodging.Id, lodging);
            lodgingRepositoryMock.VerifyAll();
            Assert.IsTrue(resultOfUpdate.Name.Equals("San Ramon Hotel"));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInUpdateLodgingTest()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            lodgingRepositoryMock.Setup(m => m.Update(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            Lodging resultOfUpdate = lodgingLogic.UpdateLodging(lodging.Id, lodging);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInUpdateNullLodgingTest()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);
            lodgingRepositoryMock.Setup(m => m.Update(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.Save());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            Lodging resultOfUpdate = lodgingLogic.UpdateLodging(lodging.Id, lodging);
        }

        [TestMethod]
        public void RemoveValidLodgingTest()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);
            lodgingRepositoryMock.Setup(m => m.Remove(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Lodging>());
            lodgingRepositoryMock.Setup(m => m.Save());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            lodgingLogic.RemoveLodging(lodging.Id);
            List<Lodging> listOfLodging = lodgingLogic.GetAllLoadings();
            lodgingRepositoryMock.VerifyAll();
            Assert.IsTrue(listOfLodging.Count==0); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void RemoveInvalidLodgingTests()
        {
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            lodgingRepositoryMock.Setup(m => m.Remove(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Lodging>());
            lodgingRepositoryMock.Setup(m => m.Save());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            lodgingLogic.RemoveLodging(lodging.Id);
        }

        [TestMethod]
        public void GetAllLodgingsTestOk()
        {
            List<Lodging> lodgingsToReturn = new List<Lodging>() {lodging};

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.GetAll()).Returns(lodgingsToReturn);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);

            List<Lodging> lodgingsObteinedOfGetAll = lodgingLogic.GetAllLoadings();

            lodgingRepositoryMock.VerifyAll();

            CollectionAssert.AreEqual(lodgingsToReturn, lodgingsObteinedOfGetAll);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailAboutGetAllLodgingsTest()
        {
            List<Lodging> lodgingsToReturn = new List<Lodging>() {lodging};
            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());
            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object);
            List<Lodging> loadgingsObteinedOfGetAll = lodgingLogic.GetAllLoadings();
        }
    }
}
