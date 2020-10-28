using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ForResponse;
using Moq;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class ReportControllerTest
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
                PricePerNight = 100,
                TouristSpot = touristSpotForLodging
            };

            reserveOfLodging = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela00@gmail.com",
                DescriptionForGuest = "Se ha registrado correctamente la reserva",
                PhoneNumberOfContact = 29082733,
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1,
                LodgingOfReserve = lodgingForReserve,
                StateOfReserve = Reserve.ReserveState.Creada
            };
        }

        [TestMethod]
        public void GetLodgingsWithReservesTestOK()
        {
            lodgingForReserve.ReservesForLodging.Add(reserveOfLodging); 
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(l => l.
            GetLodgingsWithReservesBetweenDates(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Lodging>() { lodgingForReserve });
            ReportController reportController = new ReportController(lodgingManagementMock.Object);

            DateTime checkInMaxDate = new DateTime(2020, 10, 05);
            DateTime checkOutMaxDate = new DateTime(2020, 10, 07); 

            var result = reportController.Get(touristSpotForLodging.Id, checkInMaxDate, checkOutMaxDate);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<ReportLodgingModelForResponse>;

            ReportLodgingModelForResponse reportResultExpected = new ReportLodgingModelForResponse()
            {
                NameOfLodging = lodgingForReserve.Name,
                QuantityOfReserves = 1
            }; 

            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(reportResultExpected, model[0]);
        }


        [TestMethod]
        public void FailInGetLodgingsWithReservesTestClientException()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(l => l.
            GetLodgingsWithReservesBetweenDates(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Throws(new ClientBusinessLogicException());
            ReportController reportController = new ReportController(lodgingManagementMock.Object);

            DateTime checkInMaxDate = new DateTime(2020, 10, 05);
            DateTime checkOutMaxDate = new DateTime(2020, 10, 07);

            var result = reportController.Get(touristSpotForLodging.Id, checkInMaxDate, checkOutMaxDate);
            var notFoundResult = result as NotFoundObjectResult;

            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void InternalServerErrorGettingLodgingsWithReservesTest()
        {
            var lodgingManagementMock = new Mock<ILodgingManagement>(MockBehavior.Strict);
            lodgingManagementMock.Setup(l => l.
            GetLodgingsWithReservesBetweenDates(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Throws(new ServerBusinessLogicException());
            ReportController reportController = new ReportController(lodgingManagementMock.Object);

            DateTime checkInMaxDate = new DateTime(2020, 10, 05);
            DateTime checkOutMaxDate = new DateTime(2020, 10, 07);

            var result = reportController.Get(touristSpotForLodging.Id, checkInMaxDate, checkOutMaxDate);
            var internalErrorResult = result as ObjectResult;

            lodgingManagementMock.VerifyAll();
            Assert.AreEqual(500, internalErrorResult.StatusCode);
        }

    }
}
