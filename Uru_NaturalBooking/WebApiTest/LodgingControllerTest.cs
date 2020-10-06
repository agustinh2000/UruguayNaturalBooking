using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ForRequest;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class LodgingControllerTest
    {
        TouristSpotModelForLodgingResponseModel lodgingTouristSpotModel;
        LodgingModelForResponse lodgingModelForResponse;
        LodgingModelForRequest lodgingModelForRequest;
        Lodging lodgingOfficial;
        Lodging lodgingForGet; 
        Region regionForTouristSpot;
        Category category;
        TouristSpot touristSpotAdded;

        [TestInitialize]
        public void SetUp()
        {
            lodgingTouristSpotModel = new TouristSpotModelForLodgingResponseModel()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este"
            };

            lodgingModelForResponse = new LodgingModelForResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel las cumbres",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                LodgingTouristSpotModel = lodgingTouristSpotModel
            };

            lodgingModelForRequest = new LodgingModelForRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel las cumbres",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpotId = lodgingTouristSpotModel.Id
            };

            regionForTouristSpot = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur,
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

            lodgingOfficial = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel las cumbres",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            lodgingForGet = new Lodging()
            {
                Id = lodgingModelForResponse.Id,
                Name = "Hotel las cumbres",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };
        }

        [TestMethod]
        public void GetLodgingByIdTestOk()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetLodgingById(It.IsAny<Guid>())).Returns(lodgingForGet);
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);

            var result = lodgingController.Get(lodgingModelForResponse.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as LodgingModelForResponse;

            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(lodgingModelForResponse, model);
        }

        [TestMethod]
        public void GetLodgingByIdNotFound()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetLodgingById(It.IsAny<Guid>())).Returns(value: null);
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Get(lodgingModelForResponse.Id);
            var createdResult = result as NotFoundObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetLodgingByIdInternalErrorTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetLodgingById(It.IsAny<Guid>())).Throws(new ExceptionBusinessLogic("El hospedaje solicitado no fue encontrado."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Get(lodgingModelForResponse.Id);
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAllLodgingsTestOk()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetAllLoadings()).Returns(new List<Lodging> { lodgingForGet });
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Get();
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<LodgingModelForResponse>;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(lodgingModelForResponse, model.First());
        }

        [TestMethod]
        public void GetAllLodgingsNotFoundTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetAllLoadings()).Returns(value: null);
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Get();
            var createdResult = result as NotFoundObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAllLodgingsInternalErrorTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetAllLoadings()).Throws(new ExceptionBusinessLogic("No se pudo encontrar hospedajes."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Get();
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateLodgingTestOk()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<Guid>())).Returns(lodgingOfficial);
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Post(lodgingModelForRequest);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as LodgingModelForResponse;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(LodgingModelForResponse.ToModel(lodgingOfficial), model);
        }

        [TestMethod]
        public void CreateLodgingInvalidTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<Guid>())).Throws(new ExceptionBusinessLogic("No se puede crear el hospedaje debido a que no es valido."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Post(lodgingModelForRequest);
            var createdResult = result as BadRequestObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void UpdateLodgingTestOk()
        {
            Lodging lodgingUpdated = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel Enjoy Conrad",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            LodgingModelForRequest lodgingModelForRequestToUpdate = new LodgingModelForRequest()
            {
                Id= lodgingModelForRequest.Id, 
                Name = "Hotel Enjoy Conrad"
            }; 

            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.UpdateLodging(It.IsAny<Lodging>())).Returns(lodgingUpdated);
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Put(lodgingModelForRequestToUpdate);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as LodgingModelForResponse;
            lodgingManagementMock.VerifyAll();

            LodgingModelForResponse lodgingModelForResponse = new LodgingModelForResponse()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel Enjoy Conrad",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                LodgingTouristSpotModel = TouristSpotModelForLodgingResponseModel.ToModel(touristSpotAdded)
            };

            Assert.AreEqual(lodgingModelForResponse, model);
        }

        [TestMethod]
        public void UpdateInvalidLodgingTest()
        {
            Lodging lodgingUpdated = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            LodgingModelForRequest lodgingModelForRequestToUpdate = new LodgingModelForRequest()
            {
                Id = lodgingModelForRequest.Id,
                Name = ""
            };

            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.UpdateLodging(It.IsAny<Lodging>())).Throws(new ExceptionBusinessLogic("No se puede actualizar el hospedaje deseado."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Put(lodgingModelForRequestToUpdate);
            var createdResult = result as BadRequestObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void DeleteValidLodgingTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.RemoveLodging(It.IsAny<Guid>()));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Delete(lodgingModelForRequest.Id); 
            var createdResult = result as NoContentResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(204, createdResult.StatusCode);
        }

        [TestMethod]
        public void DeleteLodgingTestWithInternalError()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.RemoveLodging(It.IsAny<Guid>())).Throws(new ExceptionBusinessLogic("No se puede eliminar el hospedaje."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Delete(lodgingModelForRequest.Id);
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

    }
}
