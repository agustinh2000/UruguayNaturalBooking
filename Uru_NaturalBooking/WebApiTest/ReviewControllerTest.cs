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
    public class ReviewControllerTest
    {

        TouristSpot touristSpot;

        Lodging lodging;

        Reserve reserve;

        Review review;

        ReviewModelForRequest reviewForRequest;

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

            reviewForRequest = new ReviewModelForRequest()
            {
                Description = "Me gusto mucho la estadia",
                IdOfReserveAssociated = reserve.Id,
                Score = 4
            };
        }
       


        [TestMethod]
        public void GetReviewTestOk()
        {

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(review);
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Get(review.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as ReviewModelForResponse;

            var expected = ReviewModelForResponse.ToModel(review);

            mock.VerifyAll();
            Assert.AreEqual(expected, model);
        }

        [TestMethod]
        public void GetReviewNotFoundFailedTest()
        {

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Get(review.Id);
            var createdResult = result as NotFoundObjectResult;
            mock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }


        [TestMethod]
        public void GetReviewInternalErrorFailedTest()
        {

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Get(review.Id);
            var createdResult = result as ObjectResult;
            mock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }


        [TestMethod]
        public void CreateReviewOk()
        {

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Review>(), reserve.Id)).Returns(review);
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Post(reviewForRequest);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as ReviewModelForResponse;
            var expected = ReviewModelForResponse.ToModel(review);

            mock.VerifyAll();
            Assert.AreEqual(expected, model);
        }

        [TestMethod]
        public void CreateReviewEmptyDescriptionFailedTest()
        {

            reviewForRequest.Description = "";

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Review>(), reserve.Id)).Throws(new DomainBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Post(reviewForRequest);
            var createdResult = result as BadRequestObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateReviewScoreLessThanOneFailedTest()
        {

            reviewForRequest.Score = 0;

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Review>(), reserve.Id)).Throws(new DomainBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Post(reviewForRequest);
            var createdResult = result as BadRequestObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateReviewScoreGreaterThanFiveFailedTest()
        {

            reviewForRequest.Score = 6;

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Review>(), reserve.Id)).Throws(new DomainBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Post(reviewForRequest);
            var createdResult = result as BadRequestObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateReviewScoreNotFoundAssociatedReserveFailedTest()
        {

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Review>(), reserve.Id)).Throws(new ClientBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Post(reviewForRequest);
            var createdResult = result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void CreateReviewInternalErrorFailedTest()
        {

            var mock = new Mock<IReviewManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Review>(), reserve.Id)).Throws(new ServerBusinessLogicException());
            ReviewController reviewController = new ReviewController(mock.Object);

            var result = reviewController.Post(reviewForRequest);
            var createdResult = result as ObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }
    }
}
