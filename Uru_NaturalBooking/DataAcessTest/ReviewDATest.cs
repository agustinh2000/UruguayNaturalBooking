using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAcessTest
{
    [TestClass]
    public class ReviewDATest
    {

        TouristSpot touristSpot;

        Lodging lodging;

        Reserve reserve;

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
        }


        [TestMethod]
        public void TestAddReviewOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IReviewRepository reviewRepo = new ReviewRepository(context);
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            ILodgingRepository lodgingRepo = new LodgingRepository(context);

            IRepository<Reserve> reserveRepo = new BaseRepository<Reserve>(context);

            touristSpotRepo.Add(touristSpot);
            lodgingRepo.Add(lodging);
            reserveRepo.Add(reserve);

            Review reviewToAdd = new Review()
            {
                Id = Guid.NewGuid(),
                Description = "Me gusto mucho la estadia",
                IdOfReserve = reserve.Id,
                LastNameOfWhoComments = reserve.LastName,
                NameOfWhoComments = reserve.Name,
                LodgingOfReview = lodging,
                Score = 4
            };
            reviewRepo.Add(reviewToAdd);
            List<Review> listOfReviews = reviewRepo.GetAll().ToList();

            Assert.IsTrue(reviewToAdd.Equals(listOfReviews[0]));
        }

        [TestMethod]
        public void GetReviewByReserveIdTest()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IReviewRepository reviewRepo = new ReviewRepository(context);
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            ILodgingRepository lodgingRepo = new LodgingRepository(context);

            IRepository<Reserve> reserveRepo = new BaseRepository<Reserve>(context);

            touristSpotRepo.Add(touristSpot);
            lodgingRepo.Add(lodging);
            reserveRepo.Add(reserve);

            Review reviewToAdd = new Review()
            {
                Id = Guid.NewGuid(),
                Description = "Me gusto mucho la estadia",
                IdOfReserve = reserve.Id,
                LastNameOfWhoComments = reserve.LastName,
                NameOfWhoComments = reserve.Name,
                LodgingOfReview = lodging,
                Score = 4
            };
            reviewRepo.Add(reviewToAdd);
            Review reviewResult = reviewRepo.GetReviewByReserveId(reserve.Id);

            Assert.IsTrue(reviewResult.Equals(reviewToAdd));
        }

        [TestMethod]
        public void GetAverageReviewScoreByLodgingTest()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IReviewRepository reviewRepo = new ReviewRepository(context);
            ITouristSpotRepository touristSpotRepo = new TouristSpotRepository(context);
            ILodgingRepository lodgingRepo = new LodgingRepository(context);

            IRepository<Reserve> reserveRepo = new BaseRepository<Reserve>(context);

            touristSpotRepo.Add(touristSpot);
            lodgingRepo.Add(lodging);
            reserveRepo.Add(reserve);

            Review reviewToAdd = new Review()
            {
                Id = Guid.NewGuid(),
                Description = "Me gusto mucho la estadia",
                IdOfReserve = reserve.Id,
                LastNameOfWhoComments = reserve.LastName,
                NameOfWhoComments = reserve.Name,
                LodgingOfReview = lodging,
                Score = 4
            };
            reviewRepo.Add(reviewToAdd);
            double averageResult = reviewRepo.GetAverageReviewScoreByLodging(lodging.Id);

            Assert.AreEqual(4.0, averageResult);
        }
    }
}
