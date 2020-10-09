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
        Picture picture;

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

            picture = new Picture()
            {
                Path = "Desktop/foto.jpg"
            }; 

            touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Maldonado",
                Description = "Departamento donde la naturaleza y la tranquilidad desborda.",
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }, 
                Image= picture
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
                TouristSpot = touristSpot,
                Images= new List<Picture>() {picture}
            };
        }


        [TestMethod]
        public void CreateValidReserveTestOk()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);

            reserveRepositoryMock.VerifyAll();
            Assert.IsTrue(reserve.Equals(resultOfCreateAReserve));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void CreateInvalidReserveTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>())).Throws(new ExceptionRepository());

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutNameTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            var reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);
            reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutLastNameTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithInvalidDateTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithoutGuestTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
        }


        [TestMethod]
        [ExpectedException(typeof(ReserveException))]
        public void CreateInvalidReserveWithInvalidMailTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Add(It.IsAny<Reserve>()));

            var lodgingRepositoryMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            lodgingRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(lodging);

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);
            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            LodgingManagement lodgingLogic = new LodgingManagement(lodgingRepositoryMock.Object, touristSpotLogic);
            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object, lodgingLogic);

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

            Reserve resultOfCreateAReserve = reserveLogic.Create(reserve, lodging.Id);
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

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve resultOfGetAReserve = reserveLogic.GetById(reserve.Id);

            reserveRepositoryMock.VerifyAll();
            Assert.IsTrue(reserve.Equals(resultOfGetAReserve));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetExceptionBySearchReserveWithIdTest()
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

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve resultOfGetAReserve = reserveLogic.GetById(reserve.Id);
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

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = reserve.Id,
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            }; 

            Reserve resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);

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

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = reserve.Id,
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            Reserve resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
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

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = reserve.Id,
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            Reserve resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void UpdateInvalidBecauseReserveIsNullTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>()));

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = Guid.NewGuid(),
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            Reserve resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void ThrowsExceptionOnUpdateReserveTest()
        {
            var reserveRepositoryMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            reserveRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionBusinessLogic("Ha ocurrido un error al obtener la reserva."));
            reserveRepositoryMock.Setup(m => m.Update(It.IsAny<Reserve>()));

            ReserveManagement reserveLogic = new ReserveManagement(reserveRepositoryMock.Object);

            Reserve reserveToUpdate = new Reserve()
            {
                Id = Guid.NewGuid(),
                DescriptionForGuest = "Su reserva ha sido aceptada",
                StateOfReserve = Reserve.ReserveState.Aceptada
            };

            Reserve resultOfUpdate = reserveLogic.Update(reserveToUpdate.Id, reserveToUpdate);
        }

    }
}
