using BusinessLogic;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ForRequest;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class ReserveControllerTest
    {
        Region regionForTouristSpot;
        Category catogoryForTouristSpot;
        TouristSpot touristSpotForLodging;
        Lodging lodgingForReserve;
        Reserve reserveOfLodging; 

        [TestInitialize]
        public void SetUp()
        {
            regionForTouristSpot = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur,
            };

            catogoryForTouristSpot = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            touristSpotForLodging = new TouristSpot()
            {
                Id = Guid.NewGuid(),
                Name = "Punta del Este",
                Description = "Un lugar increible",
                Region = regionForTouristSpot,
                ListOfCategories = new List<CategoryTouristSpot>() { new CategoryTouristSpot() { Category = catogoryForTouristSpot } }
            };

            lodgingForReserve = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel las cumbres",
                Address = "En la punta de punta del este",
                QuantityOfStars = 5,
                PricePerNight = 150,
                TouristSpot = touristSpotForLodging
            };

            reserveOfLodging = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela00@gmail.com",
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1, 
                LodgingOfReserve= lodgingForReserve
            }; 
        }

        [TestMethod]
        public void CreateReserveTestOk()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Create(It.IsAny<Reserve>(), It.IsAny<Guid>())).Returns(reserveOfLodging);
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            ReserveModelForRequest reserveModelForRequest = new ReserveModelForRequest()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela00@gmail.com",
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1,
                IdOfLodgingToReserve = lodgingForReserve.Id
            }; 

            var result = reserveController.Post(reserveModelForRequest);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as Reserve;
            reserveManagementMock.VerifyAll();
            reserveOfLodging.Id = model.Id; 

            Assert.AreEqual(reserveOfLodging, model);
        }

        [TestMethod]
        public void CreateInvalidReserveTestOk()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Create(It.IsAny<Reserve>(), It.IsAny<Guid>())).
                Throws(new ExceptionBusinessLogic("No se pudo crear correctamente la reserva"));
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            ReserveModelForRequest reserveModelForRequest = new ReserveModelForRequest()
            {
                Name = "",
                LastName = "Lamela",
                Email = "joaquin.lamela00@gmail.com",
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1,
                IdOfLodgingToReserve = lodgingForReserve.Id
            };

            var result = reserveController.Post(reserveModelForRequest);
            var createdResult = result as BadRequestObjectResult;
            reserveManagementMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }



    }
}
