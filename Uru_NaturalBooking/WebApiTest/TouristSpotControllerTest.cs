using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ForRequest;
using Model.ForResponse;
using Model.ForResponseAndRequest;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class TouristSpotControllerTest
    {
        Region regionForTouristSpot;
        Category category;
        TouristSpot touristSpotAdded;
        TouristSpotForRequestModel touristSpotRequestModel;
        TouristSpotForResponseModel touristSpotResponseModel;

        [TestInitialize]
        public void SetUp()
        {

            regionForTouristSpot = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            touristSpotAdded = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del Este",
                Description = "Un lugar increible",
                Region = regionForTouristSpot,
                ListOfCategories = new List<CategoryTouristSpot>() { new CategoryTouristSpot() { Category = category } }
            };

            touristSpotRequestModel = new TouristSpotForRequestModel()
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del Este",
                Description = "Un lugar increible",
                RegionId = regionForTouristSpot.Id,
                ListOfCategoriesId = new List<Guid>() { category.Id }
            };

            touristSpotResponseModel = new TouristSpotForResponseModel
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del Este",
                Description = "Un lugar increible",
                RegionModel = RegionForResponseModel.ToModel(regionForTouristSpot),
                ListOfCategoriesModel = new List<CategoryModel>() { CategoryModel.ToModel(category) }
            };
        }

        [TestMethod]
        public void CreateTouristSpotTestOk()
        {

            TouristSpotForRequestModel touristSpotRequestModel = new TouristSpotForRequestModel()
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del Este",
                Description = "Un lugar increible",
                RegionId = regionForTouristSpot.Id,
                ListOfCategoriesId = new List<Guid>() { category.Id }
            };
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.Create(It.IsAny<TouristSpot>(), It.IsAny<Guid>(), It.IsAny<List<Guid>>())).Returns(touristSpotAdded);
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Post(touristSpotRequestModel);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TouristSpotForResponseModel;
            touristSpotMock.VerifyAll();

            TouristSpotForResponseModel touristSpotResponseModel = new TouristSpotForResponseModel()
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del Este",
                Description = "Un lugar increible",
                RegionModel = RegionForResponseModel.ToModel(regionForTouristSpot),
                ListOfCategoriesModel = new List<CategoryModel>() {CategoryModel.ToModel(category)}
            };

            Assert.AreEqual(touristSpotResponseModel, model);
        }

        [TestMethod]
        public void CreateTouristSpotNotEqualsTest()
        {

            TouristSpotForRequestModel touristSpotRequestModel = new TouristSpotForRequestModel()
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del Este",
                Description = "Un lugar increible",
                RegionId = regionForTouristSpot.Id,
                ListOfCategoriesId = new List<Guid>() { category.Id }
            };
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.Create(It.IsAny<TouristSpot>(), It.IsAny<Guid>(), It.IsAny<List<Guid>>())).Returns(touristSpotAdded);
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Post(touristSpotRequestModel);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TouristSpotForResponseModel;
            touristSpotMock.VerifyAll();

            TouristSpotForResponseModel touristSpotResponseModel = new TouristSpotForResponseModel()
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del ",
                Description = "Un lugar increible",
                RegionModel = RegionForResponseModel.ToModel(regionForTouristSpot),
                ListOfCategoriesModel = new List<CategoryModel>() { CategoryModel.ToModel(category) }
            };

            Assert.AreNotEqual(touristSpotRequestModel, model);
        }

        [TestMethod]
        public void CreateTouristSpotNotEqualsIdTest()
        {

            TouristSpotForRequestModel touristSpotRequestModel = new TouristSpotForRequestModel()
            {
                Id = touristSpotAdded.Id,
                Name = "Punta del Este",
                Description = "Un lugar increible",
                RegionId = regionForTouristSpot.Id,
                ListOfCategoriesId = new List<Guid>() { category.Id }
            };
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.Create(It.IsAny<TouristSpot>(), It.IsAny<Guid>(), It.IsAny<List<Guid>>())).Returns(touristSpotAdded);
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Post(touristSpotRequestModel);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TouristSpotForResponseModel;
            touristSpotMock.VerifyAll();

            TouristSpotForResponseModel touristSpotResponseModel = new TouristSpotForResponseModel()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del ",
                Description = "Un lugar increible",
                RegionModel = RegionForResponseModel.ToModel(regionForTouristSpot),
                ListOfCategoriesModel = new List<CategoryModel>() { CategoryModel.ToModel(category) }
            };

            Assert.AreNotEqual(touristSpotRequestModel, model);
        }

        [TestMethod]
        public void InvalidCreateTouristSpotTestFormatError()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.Create(It.IsAny<TouristSpot>(), It.IsAny<Guid>(), It.IsAny<List<Guid>>())).
                Throws(new DomainBusinessLogicException("No se puede crear el punto turistico debido a que no es valido."));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Post(touristSpotRequestModel);
            var createdResult = result as BadRequestObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void InvalidCreateTouristSpotTestWithoutCategoriesAndRegions()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.Create(It.IsAny<TouristSpot>(), It.IsAny<Guid>(), It.IsAny<List<Guid>>())).
                Throws(new ClientBusinessLogicException("No se pudo encontrar la region y/o las categorias asociadas"));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Post(touristSpotRequestModel);
            var createdResult = result as NotFoundObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void InvalidCreateTouristSpotTestInternalServerError()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.Create(It.IsAny<TouristSpot>(), It.IsAny<Guid>(), It.IsAny<List<Guid>>())).
                Throws(new ServerBusinessLogicException());
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Post(touristSpotRequestModel);
            var createdResult = result as ObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByIdTestOk()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotById(It.IsAny<Guid>())).Returns(touristSpotAdded);
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Get(touristSpotResponseModel.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as TouristSpotForResponseModel;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(touristSpotResponseModel, model);
        }

        [TestMethod]
        public void GetTouristSpotByIdNotFoundTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotById(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Get(touristSpotResponseModel.Id);
            var createdResult = result as NotFoundObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByIdInternalErrorTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotById(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("No se puede obtener el punto turistico a traves del Id."));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Get(touristSpotResponseModel.Id);
            var createdResult = result as ObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAllTouristSpotsOkTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetAllTouristSpot()).Returns(new List<TouristSpot> () { touristSpotAdded});
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Get();
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<TouristSpotForResponseModel>;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(model.First(), touristSpotResponseModel);
        }

        [TestMethod]
        public void GetAllTouristSpotsNotFoundTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetAllTouristSpot()).Throws(new ClientBusinessLogicException());
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Get();
            var createdResult = result as NotFoundObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAllTouristSpotsInternalErrorTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetAllTouristSpot()).Throws(new ServerBusinessLogicException("Error. No se pueden obtener todos los puntos turisticos."));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.Get();
            var createdResult = result as ObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByRegionIdOkTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotByRegion(It.IsAny<Guid>())).Returns(new List<TouristSpot> { touristSpotAdded });
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.GetTouristSpotsByRegionId(regionForTouristSpot.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<TouristSpotForResponseModel>;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(touristSpotResponseModel, model.First());
        }

        [TestMethod]
        public void GetTouristSpotByRegionIdInternalErrorTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotByRegion(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("No se puede obtener el punto turistico por la region."));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.GetTouristSpotsByRegionId(regionForTouristSpot.Id);
            var createdResult = result as ObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByRegionIdNotFoundTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotByRegion(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            var result = touristSpotController.GetTouristSpotsByRegionId(regionForTouristSpot.Id);
            var createdResult = result as NotFoundObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByCategoriesIdOkTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotsByCategories(It.IsAny<List<Guid>>())).Returns(new List<TouristSpot> { touristSpotAdded });
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            Guid[] categories = new Guid[] { category.Id };
            var result = touristSpotController.GetTouristSpotsByCategoriesId(categories);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<TouristSpotForResponseModel>;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(touristSpotResponseModel, model.First());
        }

        [TestMethod]
        public void GetTouristSpotByCategoriesIdNotFoundTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotsByCategories(It.IsAny<List<Guid>>())).Throws(new ClientBusinessLogicException());
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            Guid[] categories = new Guid[] { category.Id };
            var result = touristSpotController.GetTouristSpotsByCategoriesId(categories);
            var createdResult = result as NotFoundObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByCategoriesIdInternalErrorTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotsByCategories(It.IsAny<List<Guid>>())).
                Throws(new ServerBusinessLogicException("No se puede obtener los puntos turisticos que se estan buscando por dichas categorias."));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            Guid[] categories = new Guid[] { category.Id };
            var result = touristSpotController.GetTouristSpotsByCategoriesId(categories);
            var createdResult = result as ObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByCategoriesAndRegionIdOkTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotsByCategoriesAndRegion(It.IsAny<List<Guid>>(), It.IsAny<Guid>())).Returns(new List<TouristSpot> { touristSpotAdded });
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            Guid[] categories = new Guid[] { category.Id };
            var result = touristSpotController.GetTouristSpotsByCategoriesAndRegionId(categories, regionForTouristSpot.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<TouristSpotForResponseModel>;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(touristSpotResponseModel, model.First());
        }

        [TestMethod]
        public void GetTouristSpotByCategoriesAndRegionIdNotFoundTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotsByCategoriesAndRegion(It.IsAny<List<Guid>>(), It.IsAny<Guid>()))
                .Throws(new ClientBusinessLogicException());
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            Guid[] categories = new Guid[] { category.Id };
            var result = touristSpotController.GetTouristSpotsByCategoriesAndRegionId(categories, regionForTouristSpot.Id);
            var createdResult = result as NotFoundObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetTouristSpotByCategoriesAndRegionIdInternalErrorTest()
        {
            var touristSpotMock = new Mock<ITouristSpotManagement>(MockBehavior.Strict);
            touristSpotMock.Setup(m => m.GetTouristSpotsByCategoriesAndRegion(It.IsAny<List<Guid>>(), It.IsAny<Guid>()))
                .Throws(new ServerBusinessLogicException("No se puede obtener los puntos turisticos que se estan buscando por dichas categorias y region."));
            TouristSpotController touristSpotController = new TouristSpotController(touristSpotMock.Object);
            Guid[] categories = new Guid[] { category.Id };
            var result = touristSpotController.GetTouristSpotsByCategoriesAndRegionId(categories, regionForTouristSpot.Id);
            var createdResult = result as ObjectResult;
            touristSpotMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }
    }
}
