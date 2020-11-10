using BusinessLogic;
using BusinessLogicException;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class LodgingTestForImportation
    {
        TouristSpot touristSpot;
        Lodging lodging;
        CategoryTouristSpot categoryTouristSpot;
        Category aCategory;
        Picture picture;
        TouristSpot touristSpotOfPuntaDelEste;
        Lodging lodgingConrad;
        LodgingPicture lodgingPicture;
        List<string> listOfPicturesPath;
        Region region; 


        [TestInitialize]
        public void SetUp()
        {
            region = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
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

            picture = new Picture()
            {
                Path = "Desktop/joaco/foto.jpg"
            };

            listOfPicturesPath = new List<string>() { picture.Path };

            touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Maldonado",
                Description = "Departamento donde la naturaleza y la tranquilidad desborda.",
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot },
                Image = picture, 
                Region = region
            };

            categoryTouristSpot.TouristSpot = touristSpot;
            categoryTouristSpot.TouristSpotId = touristSpot.Id;

            lodgingPicture = new LodgingPicture()
            {
                Picture = picture,
                PictureId = picture.Id
            };

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Las Cumbres",
                Description = "Magnifico hospedaje",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150,
                TouristSpot = touristSpot,
                Images = new List<LodgingPicture> { lodgingPicture }
            };

            touristSpotOfPuntaDelEste = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Donde el lujo y la naturaleza convergen, las mejores playas del Uruguay."
            };

            lodgingConrad = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Enjoy Conrad",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 1500,
                TouristSpot = touristSpotOfPuntaDelEste
            };
        }

        [TestMethod]
        public void CreateValidLodgingTestOk()
        {
            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogicForImportation = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogicForImportation.Create(lodging, touristSpot, listOfPicturesPath);

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
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingAlredyExistTest()
        {
            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateInvalidLodgingInternalErrorWhenSearchLodgingByNameTest()
        {
            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Throws(new ServerException());

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        public void CreateValidLodgingWithoutTouristSpotTest()
        {
            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(It.IsAny<string>())).Returns(value: null);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));

            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(region);
            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            var categoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(aCategory);
            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);

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
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithExpensivePriceTest()
        {
            lodging.PricePerNight = 15000000;

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);


            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithInvalidPriceTest()
        {
            lodging.PricePerNight = -10;

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithMoreStarsTest()
        {
            lodging.QuantityOfStars = 15;

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithoutQuantityOfStarsTest()
        {
            lodging.QuantityOfStars = 0;

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithoutDescriptionTest()
        {
            lodging.Description = "";

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithoutAddressTest()
        {
            lodging.Address = "";

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateInvalidLodgingWithErrorInAddTest()
        {

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>())).Throws(new ServerException());
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithoutNameTest()
        {
            lodging.Name = "";

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithNewListOfPicturesTest()
        {
            lodging.Images = new List<LodgingPicture>();

            List<string> listOfPictures = new List<string>();

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPictures);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidLodgingWithoutPicturesTest()
        {
            lodging.Images = null;

            List<string> listOfPictures = new List<string>();

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPictures);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void CreateInvalidLodgingWithoutRegionOnTouristSpotTest()
        {
            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Add(It.IsAny<Lodging>()));
            lodgingRepositoryMock.Setup(m => m.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id)).Returns(value: null);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(It.IsAny<string>())).Returns(value: null);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));

            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            var categoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(aCategory);
            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            TouristSpotManagement touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            LodgingManagementForImportation lodgingLogic = new LodgingManagementForImportation(lodgingRepositoryMock.Object, touristSpotLogic);

            Lodging resultOfCreateALodging = lodgingLogic.Create(lodging, touristSpot, listOfPicturesPath);
        }

    }
}
