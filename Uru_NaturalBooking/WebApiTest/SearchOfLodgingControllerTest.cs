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
using System.Text;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class SearchOfLodgingControllerTest
    {
        Lodging lodgingOfConrad;
        Picture image;
        Lodging lodgingOfCumbres; 
        Region regionForTouristSpot;
        Category category;
        TouristSpot touristSpotAdded;
        SearchOfLodgingModelForRequest searchOfLodgingModel; 

        [TestInitialize]
        public void SetUp()
        {
            image = new Picture()
            {
                Path = "c:/MiDirectorio/Imagenes/Paisaje.png"
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
                Name = "Maldonado",
                Description = "Un lugar increible",
                Region = regionForTouristSpot,
                ListOfCategories = new List<CategoryTouristSpot>() { new CategoryTouristSpot() { Category = category } }
            };

            lodgingOfConrad = new Lodging()
            {
                Images = new List<Picture>() { image },
                Id = Guid.NewGuid(),
                Name = "Hotel Enjoy Conrad",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 100,
                TouristSpot = touristSpotAdded
                
            };

            lodgingOfCumbres = new Lodging()
            {
                Images = new List<Picture>() { image },
                Id = Guid.NewGuid(),
                Name = "Hotel Las Cumbres",
                Address = "Un lugar para descansar con la pareja",
                QuantityOfStars = 4,
                PricePerNight = 200,
                TouristSpot = touristSpotAdded
            };

            searchOfLodgingModel = new SearchOfLodgingModelForRequest()
            {
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfChilds = 1,
                QuantityOfBabies = 1,
                TouristSpotIdSearch = touristSpotAdded.Id
            }; 
        }


        [TestMethod]
        public void SearchLodgingsTestOk()
        {
            List<Lodging> listOfLodgingsAvailables = new List<Lodging>() { lodgingOfCumbres, lodgingOfConrad }; 
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetAvailableLodgingsByTouristSpot(It.IsAny<Guid>())).Returns(listOfLodgingsAvailables);
            SearchOfLodgingController searchOfLodgingController = new SearchOfLodgingController(lodgingManagementMock.Object); 
            var result = searchOfLodgingController.Post(searchOfLodgingModel);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<LodgingForSearchModel>;
            lodgingManagementMock.VerifyAll();
            List<LodgingForSearchModel> listOfModelOfSearch = new List<LodgingForSearchModel>();

            LodgingForSearchModel lodgingModelOfCumbres = new LodgingForSearchModel()
            {
                CheckIn = searchOfLodgingModel.CheckIn,
                CheckOut = searchOfLodgingModel.CheckOut,
                Lodging = LodgingModelForResponse.ToModel(lodgingOfCumbres),
                TotalPriceForSearch = 700.0
            };

            LodgingForSearchModel lodgingModelOfEnjoyConrad = new LodgingForSearchModel()
            {
                CheckIn = searchOfLodgingModel.CheckIn,
                CheckOut = searchOfLodgingModel.CheckOut,
                Lodging = LodgingModelForResponse.ToModel(lodgingOfConrad),
                TotalPriceForSearch = 350.0
            };
            listOfModelOfSearch.Add(lodgingModelOfCumbres);
            listOfModelOfSearch.Add(lodgingModelOfEnjoyConrad); 
            CollectionAssert.AreEqual(listOfModelOfSearch, model); 
        }

        [TestMethod]
        public void SearchLodgingsTestInternalError()
        {
            List<Lodging> listOfLodgingsAvailables = new List<Lodging>() { lodgingOfCumbres, lodgingOfConrad };
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetAvailableLodgingsByTouristSpot(It.IsAny<Guid>())).
                Throws(new ServerBusinessLogicException("Hubo problemas al encontrar los hospedajes."));

            SearchOfLodgingController searchOfLodgingController = new SearchOfLodgingController(lodgingManagementMock.Object);
            var result = searchOfLodgingController.Post(searchOfLodgingModel);
            var createdResult = result as ObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void SearchLodgingsTestBadRequest()
        {
            List<Lodging> listOfLodgingsAvailables = new List<Lodging>() { lodgingOfCumbres, lodgingOfConrad };
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(m => m.GetAvailableLodgingsByTouristSpot(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());

            SearchOfLodgingController searchOfLodgingController = new SearchOfLodgingController(lodgingManagementMock.Object);
            var result = searchOfLodgingController.Post(searchOfLodgingModel);
            var createdResult = result as NotFoundObjectResult;
            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }
    }
}
