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
    public class ReserveTest
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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }
            };

            categoryTouristSpot.TouristSpot = touristSpot;
            categoryTouristSpot.TouristSpotId = touristSpot.Id;

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Las Cumbres",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150,
                TouristSpot = touristSpot
            };
        }


        [TestMethod]
        public void CreateValidReserve()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult= 2,
                QuantityOfChild=2, 
                QuantityOfBaby=1
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);

            reserveRepositoryMock.VerifyAll();
            Assert.AreEqual(lodging, resultOfCreateAReserve.LodgingOfReserve);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void CreateInvalidReserve()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>())).Throws(new ExceptionRepository());
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutName()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutLastName()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithInvalidDate()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 08, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutGuest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 0,
                QuantityOfChild = 0,
                QuantityOfBaby = 0
            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }


        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithInvalidMail()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var lodgingRepositoryMock = new Mock<IRepository<Lodging>>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

            Reserve reserve = new Reserve()
            {
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 1,
                QuantityOfChild = 0,
                QuantityOfBaby = 0

            };

            var resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        public void GetValidReserveByIdTest()
        {
            Reserve reserve = new Reserve()
            {
                Id= Guid.NewGuid(), 
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1, 
                LodgingOfReserve= lodging
            };

            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            var resultOfGetAReserve = reserveLogic.GetById(reserve.Id);

            reserveRepositoryMock.VerifyAll();
            Assert.IsTrue(reserve.Equals(resultOfGetAReserve));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetExceptionBySearchReserveWithId()
        {
            Reserve reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1,
                LodgingOfReserve = lodging
            };

            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            var resultOfGetAReserve = reserveLogic.GetById(reserve.Id);
        }

        [TestMethod]
        public void UpdateTheDescriptionAndTheStateOfReserveTest()
        {
            Reserve reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1,
                LodgingOfReserve = lodging, 
                StateOfReserve= Reserve.ReserveState.Creada,
                PhoneNumberOfContact= 29082733, 
                DescriptionForGuest= "Va a disfrutar de su estadia, garantia asegurada"
            };

            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save()); 

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = reserve.Id,
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            }; 

            var resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);

            reserve.DescriptionForGuest = "Su reserva ha sido aceptada";
            reserve.StateOfReserve = Reserve.ReserveState.Creada; 

            reserveRepositoryMock.VerifyAll();
            Assert.IsTrue(reserve.Equals(resultOfUpdate)); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void ThrowExceptionInUpdateMethodTest()
        {
            Reserve reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1,
                LodgingOfReserve = lodging,
                StateOfReserve = Reserve.ReserveState.Creada,
                PhoneNumberOfContact = 29082733,
                DescriptionForGuest = "Va a disfrutar de su estadia, garantia asegurada"
            };

            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>())).Throws(new ExceptionBusinessLogic("Ocurrio un error al actualizar."));
            reserveRepositoryMock.Setup(m => m.Save());

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = reserve.Id,
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            var resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void ThrowExceptionRepositoryInUpdateMethodTest()
        {
            Reserve reserve = new Reserve()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                Email = "joaquin.lamela@hotmail.com",
                CheckIn = new DateTime(2020, 05, 25),
                CheckOut = new DateTime(2020, 06, 10),
                QuantityOfAdult = 2,
                QuantityOfChild = 2,
                QuantityOfBaby = 1,
                LodgingOfReserve = lodging,
                StateOfReserve = Reserve.ReserveState.Creada,
                PhoneNumberOfContact = 29082733,
                DescriptionForGuest = "Va a disfrutar de su estadia, garantia asegurada"
            };

            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>())).Throws(new ExceptionRepository("Ocurrio un error al actualizar."));
            reserveRepositoryMock.Setup(m => m.Save());

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = reserve.Id,
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            var resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void UpdateInvalidBecauseReserveIsNullTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = Guid.NewGuid(),
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            var resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void ThrowsExceptionOnUpdateReserveTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionBusinessLogic("Ha ocurrido un error al obtener la reserva."));
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>()));
            reserveRepositoryMock.Setup(m => m.Save());

            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = Guid.NewGuid(),
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            var resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }

    }
}
