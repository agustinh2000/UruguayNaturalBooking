using BusinessLogic;
using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryException;
using System;


namespace BusinessLogicTest
{
    [TestClass]
    public class ReviewTest
    {
        TouristSpot touristSpot;

        Lodging lodging;

        Reserve reserve;

        Review review;

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Maldonado",
                Description = "Departamento donde la naturaleza y la tranquilidad desborda."
            };

            lodging = new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Hotel Las Cumbres",
                QuantityOfStars = 5,
                Address = "Ruta 12 km 3.5",
                PricePerNight = 150,
                TouristSpot = touristSpot,
            };

            reserve = new Reserve()
            {
                CheckIn = new DateTime(2020, 10, 28),
                CheckOut = new DateTime(2020, 10, 28),
                DescriptionForGuest = "Bienvenido",
                Email = "agustin@gmail.com",
                Id = Guid.NewGuid(),
                Name = "Agustin",
                LastName = "Her",
                LodgingOfReserve = lodging,
                PhoneNumberOfContact = 59866545,
                QuantityOfAdult = 1,
                QuantityOfBaby = 1,
                QuantityOfChild = 1,
                QuantityOfRetired = 2,
                StateOfReserve = Reserve.ReserveState.Creada,
                TotalPrice = 1230
            };

            review = new Review()
            {
                Id = Guid.NewGuid(),
                Description = "Me gusto mucho la estadia",
                IdOfReserve = reserve.Id,
                LastNameOfWhoComments = reserve.LastName,
                NameOfWhoComments = reserve.Name,
                LodgingOfReview = lodging,
                Score = 4
            };
        }


        [TestMethod]
        public void GetReviewByIdTestOk()
        {

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            reviewMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(review);

            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object);
            Review reviewResult = reviewLogic.GetById(review.Id);

            reviewMock.VerifyAll();
            Assert.IsTrue(review.Equals(reviewResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]

        public void GetReviewByNotExistIdTestFailed()
        {
            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            reviewMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientException());

            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object);
            Review reviewResult = reviewLogic.GetById(review.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]

        public void GetReviewByIdInternalErrorTestFailed()
        {
            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            reviewMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ServerException());

            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object);
            Review reviewResult = reviewLogic.GetById(review.Id);
        }

        [TestMethod]
        public void CreateReviewTestOk()
        {

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

            reviewMock.VerifyAll();
            Assert.IsTrue(review.Equals(reviewResult));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateReviewEmptyDescriptionFailedTest()
        {

            review.Description = "";

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateReviewEmptyNameOfWhoCommentsFailedTest()
        {

            reserve.Name = "";

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateReviewEmptyLastNameOfWhoCommentsFailedTest()
        {

            reserve.LastName = "";

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateReviewWithNumberOfStarsLessThanOneFailedTest()
        {

            review.Score = 0;

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateReviewWithNumberOfStarsGreaterThanFiveFailedTest()
        {

            review.Score = 7;

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void CreateReviewWithInvalidReserveCodeFailedTest()
        {

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientException());
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value: null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateReviewThatAlredyExistForThisCodeReserveFailedTest()
        {

            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(review);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateReviewInternalErrorCheckingIfExistReviewForReserveFailedTest()
        {
            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>()));
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value:null);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Throws(new ServerException());
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateReviewInternalErrorWhenAddReviewFailedTest()
        {
            var reviewMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reserveMock = new Mock<IRepository<Reserve>>(MockBehavior.Strict);
            var lodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);

            reviewMock.Setup(m => m.Add(It.IsAny<Review>())).Throws(new ServerException());
            reserveMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(reserve);
            reviewMock.Setup(m => m.GetReviewByReserveId(It.IsAny<Guid>())).Returns(value:null);
            reviewMock.Setup(m => m.GetAverageReviewScoreByLodging(It.IsAny<Guid>())).Returns(4.0);
            lodgingMock.Setup(m => m.Update(lodging));

            IReserveManagement reserveLogic = new ReserveManagement(reserveMock.Object);
            ILodgingManagement lodgingLogic = new LodgingManagement(lodgingMock.Object);


            ReviewManagement reviewLogic = new ReviewManagement(reviewMock.Object, reserveLogic, lodgingLogic);
            Review reviewResult = reviewLogic.Create(review, reserve.Id);
        }
    }
}
