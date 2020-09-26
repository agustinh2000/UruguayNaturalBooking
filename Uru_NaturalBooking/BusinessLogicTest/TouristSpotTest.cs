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

namespace BusinessLogicTest
{
    [TestClass]
    public class TouristSpotTest
    {
        [TestMethod]
        public void CreateValidTouristSpot()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id= Guid.NewGuid(), 
                Name= "Punta del este",
                Description= "Lo mejor para gastar."
            };
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            mock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var mock2 = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock2.Setup(m => m.Get(id)).Returns(new Category() { Id= id});

            var mock3 = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock3.Setup(m => m.Get(regionId)).Returns(new Region() { Id= regionId });

            var categoryLogic = new CategoryManagement(mock2.Object);
            var regionLogic = new RegionManagement(mock3.Object); 

            var touristSpotLogic = new TouristSpotManagement(mock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot,regionId, listIdCategories);

            mock.VerifyAll();
            Assert.AreEqual(result.Id, touristSpot.Id); 
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void CreateInValidTouristSpot()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "",
                Description = "Lo mejor para gastar."
            };
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            mock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var mock2 = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock2.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var mock3 = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock3.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(mock2.Object);
            var regionLogic = new RegionManagement(mock3.Object);

            var touristSpotLogic = new TouristSpotManagement(mock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void CreateInValidTouristSpotWithoutDesc()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = ""
            };
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            mock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var mock2 = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock2.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var mock3 = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock3.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(mock2.Object);
            var regionLogic = new RegionManagement(mock3.Object);

            var touristSpotLogic = new TouristSpotManagement(mock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void CreateInValidTouristSpotExtendDesc()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            mock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var mock2 = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock2.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var mock3 = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock3.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(mock2.Object);
            var regionLogic = new RegionManagement(mock3.Object);

            var touristSpotLogic = new TouristSpotManagement(mock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void CreateInValidTouristSpotWithErrorInAdd()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Donde la naturaleza y el lujo convergen"
            };
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<TouristSpot>())).Throws(new ExceptionRepository());
            mock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var mock2 = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock2.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var mock3 = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock3.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(mock2.Object);
            var regionLogic = new RegionManagement(mock3.Object);

            var touristSpotLogic = new TouristSpotManagement(mock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }


    }
}
