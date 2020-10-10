using BusinessLogic;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                PricePerNight = 100,
                TouristSpot = touristSpotForLodging
            };

            reserveOfLodging = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela00@gmail.com",
                DescriptionForGuest= "Se ha registrado correctamente la reserva", 
                PhoneNumberOfContact= 29082733, 
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1, 
                LodgingOfReserve= lodgingForReserve,
                StateOfReserve= Reserve.ReserveState.Creada
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
            var model = createdResult.Value as ReserveModelForResponse;
            reserveManagementMock.VerifyAll();

            Assert.AreEqual(ReserveModelForResponse.ToModel(reserveOfLodging), model);
        }

        [TestMethod]
        public void CreateInvalidReserveTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Create(It.IsAny<Reserve>(), It.IsAny<Guid>())).
                Throws(new DomainBusinessLogicException("No se pudo crear correctamente la reserva"));
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

        [TestMethod]
        public void CreateInvalidReserveWithClientExceptionTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Create(It.IsAny<Reserve>(), It.IsAny<Guid>())).
                Throws(new ClientBusinessLogicException("No se pudo crear correctamente la reserva"));
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
            var createdResult = result as ObjectResult;
            reserveManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void InvalidCreateReserveInternalServerErrorTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Create(It.IsAny<Reserve>(), It.IsAny<Guid>())).
                Throws(new ServerBusinessLogicException());
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
            var createdResult = result as ObjectResult;
            reserveManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetExistingReserveByIdOkTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(reserveOfLodging);
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            var result = reserveController.Get(reserveOfLodging.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as ReserveModelForResponse;

            reserveManagementMock.VerifyAll();
            Assert.AreEqual(ReserveModelForResponse.ToModel(reserveOfLodging), model); 
        }

        [TestMethod]
        public void CantGetReserveByIdBecauseNotExistTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            var result = reserveController.Get(reserveOfLodging.Id);
            var createdResult = result as NotFoundObjectResult;

            reserveManagementMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void CantGetReserveByIdInternalServerErrorTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("Ha ocurrido un error al acceder a la base de datos"));
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            var result = reserveController.Get(reserveOfLodging.Id);
            var createdResult = result as ObjectResult;

            reserveManagementMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void UpdateReserveTestOk()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<Reserve>())).Returns(reserveOfLodging);
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            ReserveModelForRequestUpdate reserveModelForRequestUpdate = new ReserveModelForRequestUpdate()
            {
                Id = reserveOfLodging.Id,
                Description = "Su reserva ha sido aceptada correctamente, por favor verifique el nuevo estado",
                StateOfReserve = Reserve.ReserveState.Aceptada
            }; 

            var result = reserveController.Put(reserveOfLodging.Id, reserveModelForRequestUpdate);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as ReserveModelForResponse;
            reserveManagementMock.VerifyAll();

            Reserve reserveToCompare = new Reserve()
            {
                Id = reserveOfLodging.Id,
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela00@gmail.com",
                DescriptionForGuest = "Su reserva ha sido aceptada correctamente, por favor verifique el nuevo estado",
                PhoneNumberOfContact = 29082733,
                CheckIn = new DateTime(2020, 10, 05),
                CheckOut = new DateTime(2020, 10, 07),
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1,
                LodgingOfReserve = lodgingForReserve,
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            Assert.AreEqual(ReserveModelForResponse.ToModel(reserveToCompare), model);
        }

        [TestMethod]
        public void FailInUpdateInternalServerErrorTest()
        {
            var reserveManagementMock = new Mock<IReserveManagement>(MockBehavior.Strict);
            reserveManagementMock.Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<Reserve>())).
                Throws(new ServerBusinessLogicException("No se ha podido actualizar correctamente la reserva"));
            ReserveController reserveController = new ReserveController(reserveManagementMock.Object);

            ReserveModelForRequestUpdate reserveModelForRequestUpdate = new ReserveModelForRequestUpdate()
            {
                Id = reserveOfLodging.Id,
                Description = "Su reserva ha sido aceptada correctamente, por favor verifique el nuevo estado",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            var result = reserveController.Put(reserveOfLodging.Id, reserveModelForRequestUpdate);
            var createdResult = result as BadRequestObjectResult;
            reserveManagementMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

    }
}
