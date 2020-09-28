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

        [TestMethod]
        public void GetValidTouristSpotByRegion()
        {
            Region region1 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Region region2 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Este
            };

            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Guid idForTouristSpot = Guid.NewGuid(); 

            CategoryTouristSpot categoryTouristSpot = new CategoryTouristSpot()
            {
                CategoryId = category.Id,
                TouristSpotId = idForTouristSpot
            };

            TouristSpot touristSpot = new TouristSpot
            {
                Id = idForTouristSpot,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() {categoryTouristSpot} 
            };

            TouristSpot touristSpot2 = new TouristSpot
            {
                Id = idForTouristSpot,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region2,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }
            };

            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot, touristSpot2 }; 

            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(listOfTouristSpotSearched);

            var touristSpotLogic = new TouristSpotManagement(mock.Object);

            var result = touristSpotLogic.GetTouristSpotByRegion(region1.Id); 

            mock.VerifyAll();
            Assert.AreEqual(touristSpot, result[0]); 
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidTouristSpotByRegion()
        {
            Region region1 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Region region2 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Este
            };

            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Guid idForTouristSpot = Guid.NewGuid();

            CategoryTouristSpot categoryTouristSpot = new CategoryTouristSpot()
            {
                CategoryId = category.Id,
                TouristSpotId = idForTouristSpot
            };

            TouristSpot touristSpot = new TouristSpot
            {
                Id = idForTouristSpot,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }
            };

            TouristSpot touristSpot2 = new TouristSpot
            {
                Id = idForTouristSpot,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }
            };

            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot, touristSpot2 };

            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());

            var touristSpotLogic = new TouristSpotManagement(mock.Object);

            var result = touristSpotLogic.GetTouristSpotByRegion(region1.Id);
        }


        [TestMethod]
        public void GetValidTouristSpotById()
        {
            Region region1 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Guid idForTouristSpot = Guid.NewGuid();

            CategoryTouristSpot categoryTouristSpot = new CategoryTouristSpot()
            {
                CategoryId = category.Id,
                TouristSpotId = idForTouristSpot
            };

            TouristSpot touristSpot = new TouristSpot
            {
                Id = idForTouristSpot,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot }
            };

            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot); 

            var touristSpotLogic = new TouristSpotManagement(mock.Object);

            var result = touristSpotLogic.GetTouristSpotById(idForTouristSpot);

            mock.VerifyAll();
            Assert.AreEqual(touristSpot, result); 

        }


        [TestMethod]
        public void GetValidTouristSpotSearchByCategories()
        {
            Region region1 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Category category1 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Category category2 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Arena"
            };

            Guid idForTouristSpot1 = Guid.NewGuid();
            Guid idForTouristSpot2 = Guid.NewGuid(); 

            CategoryTouristSpot categoryTouristSpot1 = new CategoryTouristSpot()
            {
                CategoryId = category1.Id,
                TouristSpotId = idForTouristSpot1
            };

            CategoryTouristSpot categoryTouristSpot2 = new CategoryTouristSpot()
            {
                CategoryId = category2.Id,
                TouristSpotId = idForTouristSpot2
            };

            TouristSpot touristSpot = new TouristSpot
            {
                Id = idForTouristSpot1,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1, categoryTouristSpot2 }
            };

            TouristSpot touristSpot2 = new TouristSpot
            {
                Id = idForTouristSpot2,
                Name = "Punta del diablo",
                Description = "Las mejores playas del Uruguay",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot, touristSpot2 };

            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(listOfTouristSpotSearched);

            var touristSpotLogic = new TouristSpotManagement(mock.Object);

            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id }; 

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesToSearch); 

            mock.VerifyAll();
            Assert.AreEqual(touristSpot, result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidTouristSpotSearchByCategories()
        {
            Region region1 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Category category1 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Category category2 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Arena"
            };

            Guid idForTouristSpot1 = Guid.NewGuid();
            Guid idForTouristSpot2 = Guid.NewGuid();

            CategoryTouristSpot categoryTouristSpot1 = new CategoryTouristSpot()
            {
                CategoryId = category1.Id,
                TouristSpotId = idForTouristSpot1
            };

            CategoryTouristSpot categoryTouristSpot2 = new CategoryTouristSpot()
            {
                CategoryId = category2.Id,
                TouristSpotId = idForTouristSpot2
            };

            TouristSpot touristSpot = new TouristSpot
            {
                Id = idForTouristSpot1,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1, categoryTouristSpot2 }
            };

            TouristSpot touristSpot2 = new TouristSpot
            {
                Id = idForTouristSpot2,
                Name = "Punta del diablo",
                Description = "Las mejores playas del Uruguay",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot, touristSpot2 };

            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());

            var touristSpotLogic = new TouristSpotManagement(mock.Object);

            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesToSearch);
        }




    }
}
