using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ForRequest;
using Model.ForResponse;
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
        Picture image;
        LodgingPicture lodgingPicture; 


        [TestInitialize]
        public void SetUp()
        {
            image = new Picture()
            {
                Id= Guid.NewGuid(), 
                Path = "c:/MiDirectorio/Imagenes/Paisaje.png"
            };

            lodgingPicture = new LodgingPicture()
            {
                Picture = image,
                PictureId = image.Id
            }; 


            lodgingTouristSpotModel = new TouristSpotModelForLodgingResponseModel()
            {

                Id = Guid.NewGuid(),
                Name = "Punta del este",

            };

            lodgingModelForResponse = new LodgingModelForResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel las cumbres",
                ImagesPath = new List<string> { image.Path },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                LodgingTouristSpotModel = lodgingTouristSpotModel
            };

            lodgingModelForRequest = new LodgingModelForRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel las cumbres",
                Images = new List<Picture> {image},
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
                Image = image,
                Description = "Un lugar increible",
                Region = regionForTouristSpot,
                ListOfCategories = new List<CategoryTouristSpot>() { new CategoryTouristSpot() { Category = category } }
            };

            lodgingOfficial = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel las cumbres",
                Images = new List<LodgingPicture>() { lodgingPicture },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            lodgingForGet = new Lodging()
            {
                Id = lodgingModelForResponse.Id,
                Name = "Hotel las cumbres",
                Images = new List<LodgingPicture>() { lodgingPicture },
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
            lodgingManagementMock.Setup(m => m.GetLodgingById(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
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
            lodgingManagementMock.Setup(m => m.GetLodgingById(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("El hospedaje solicitado no fue encontrado."));
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
            lodgingManagementMock.Setup(m => m.GetAllLoadings()).Throws(new ClientBusinessLogicException());
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
            lodgingManagementMock.Setup(m => m.GetAllLoadings()).Throws(new ServerBusinessLogicException("No se pudo encontrar hospedajes."));
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
            lodgingManagementMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<Guid>(), It.IsAny<List<Picture>>())).Returns(lodgingOfficial);
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
            lodgingManagementMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<Guid>(), It.IsAny<List<Picture>>())).
                Throws(new DomainBusinessLogicException("No se puede crear el hospedaje debido a que no es valido."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Post(lodgingModelForRequest);
            var createdResult = result as BadRequestObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateLodgingWithoutTouristSpotTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<Guid>(), It.IsAny<List<Picture>>())).
                Throws(new ClientBusinessLogicException("No se puede crear el hospedaje debido a que no se encontro el punto turistico"));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Post(lodgingModelForRequest);
            var createdResult = result as NotFoundObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateLodgingInternalServerErrorTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<Guid>(), It.IsAny<List<Picture>>())).Throws(new ServerBusinessLogicException("Error interno"));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Post(lodgingModelForRequest);
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void UpdateLodgingTestOk()
        {
            Lodging lodgingUpdated = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel Enjoy Conrad",
                Images = new List<LodgingPicture> { lodgingPicture },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            LodgingModelForRequest lodgingModelForRequestToUpdate = new LodgingModelForRequest()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel Enjoy Conrad",
                Images = new List<Picture> { image },

            };

            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.UpdateLodging(It.IsAny<Guid>(), It.IsAny<Lodging>())).Returns(lodgingUpdated);
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Put(lodgingModelForRequestToUpdate.Id, lodgingModelForRequestToUpdate);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as LodgingModelForResponse;
            lodgingManagementMock.VerifyAll();

            LodgingModelForResponse lodgingModelForResponse = new LodgingModelForResponse()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Hotel Enjoy Conrad",
                ImagesPath = new List<string>{ image.Path },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                LodgingTouristSpotModel = TouristSpotModelForLodgingResponseModel.ToModel(touristSpotAdded)
            };

            Assert.AreEqual(lodgingModelForResponse, model);
        }

        [TestMethod]
        public void UpdateInvalidLodgingInternalErrorTest()
        {
            Lodging lodgingUpdated = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "",
                Images = new List<LodgingPicture>() { lodgingPicture },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            LodgingModelForRequest lodgingModelForRequestToUpdate = new LodgingModelForRequest()
            {
                Id = lodgingModelForRequest.Id,
                Name = "",
                Images = new List<Picture> { image },
            };

            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.UpdateLodging(It.IsAny<Guid>(), It.IsAny<Lodging>())).
                Throws(new ServerBusinessLogicException("No se puede actualizar el hospedaje deseado."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Put(lodgingModelForRequestToUpdate.Id, lodgingModelForRequestToUpdate);
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void UpdateInvalidLodgingNotFoundTestTest()
        {
            Lodging lodgingUpdated = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "",
                Images = new List<LodgingPicture>() { lodgingPicture },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            LodgingModelForRequest lodgingModelForRequestToUpdate = new LodgingModelForRequest()
            {
                Id = lodgingModelForRequest.Id,
                Name = "Prueba",
                Images = new List<Picture> { image },
            };

            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.UpdateLodging(It.IsAny<Guid>(), It.IsAny<Lodging>())).
                Throws(new ClientBusinessLogicException("No se puede actualizar el hospedaje deseado."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Put(lodgingModelForRequestToUpdate.Id, lodgingModelForRequestToUpdate);
            var createdResult = result as NotFoundObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void UpdateInvalidLodgingWithAnyErrorFieldTest()
        {
            Lodging lodgingUpdated = new Lodging()
            {
                Id = lodgingModelForRequest.Id,
                Name = "",
                Images = new List<LodgingPicture>() { lodgingPicture },
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotAdded
            };

            LodgingModelForRequest lodgingModelForRequestToUpdate = new LodgingModelForRequest()
            {
                Id = lodgingModelForRequest.Id,
                Name = "",
                Images = new List<Picture> { image },
            };

            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.UpdateLodging(It.IsAny<Guid>(), It.IsAny<Lodging>())).
                Throws(new DomainBusinessLogicException("No se puede actualizar el hospedaje deseado."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Put(lodgingModelForRequestToUpdate.Id, lodgingModelForRequestToUpdate);
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
        public void DeleteLodgingNotFountTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.RemoveLodging(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException("El hospedaje a eliminar no ha sido encontrado."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Delete(lodgingModelForRequest.Id);
            var createdResult = result as NotFoundObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void DeleteLodgingTestWithInternalError()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.RemoveLodging(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("No se puede eliminar el hospedaje."));
            LodgingController lodgingController = new LodgingController(lodgingManagementMock.Object);
            var result = lodgingController.Delete(lodgingModelForRequest.Id);
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

    }
}
