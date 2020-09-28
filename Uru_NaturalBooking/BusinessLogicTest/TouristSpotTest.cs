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
using System.Linq;

namespace BusinessLogicTest
{
    [TestClass]
    public class TouristSpotTest
    {

        Region region1;
        Region region2; 
        Category category1;
        Category category2;
        Guid idForTouristSpot1;
        Guid idForTouristSpot2;
        Guid idForTouristSpot3; 
        CategoryTouristSpot categoryTouristSpot1;
        CategoryTouristSpot categoryTouristSpot2;
        TouristSpot touristSpot1;
        TouristSpot touristSpot2;
        TouristSpot touristSpot3; 

        [TestInitialize]
        public void SetUp()
        {
            region1 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

             region2 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Este
            };

            category1 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            category2 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Arena"
            };

             idForTouristSpot1 = Guid.NewGuid();
             idForTouristSpot2 = Guid.NewGuid();
             idForTouristSpot3 = Guid.NewGuid(); 

             categoryTouristSpot1 = new CategoryTouristSpot()
            {
                CategoryId = category1.Id,
                TouristSpotId = idForTouristSpot1
            };

             categoryTouristSpot2 = new CategoryTouristSpot()
            {
                CategoryId = category2.Id,
                TouristSpotId = idForTouristSpot2
            };

            touristSpot1 = new TouristSpot
            {
                Id = idForTouristSpot1,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                Image= new byte[10],
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1, categoryTouristSpot2 }
            };

            touristSpot2 = new TouristSpot
            {
                Id = idForTouristSpot2,
                Name = "Punta del diablo",
                Description = "Las mejores playas del Uruguay",
                Region = region2,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

             touristSpot3 = new TouristSpot
            {
                Id = idForTouristSpot3,
                Name = "Salto",
                Description = "Las mejores naranjas del Uruguay y del mundo",
                Region = region2,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };
        }


        [TestMethod]
        public void CreateValidTouristSpot()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id= Guid.NewGuid(), 
                Name= "Punta del este",
                Description= "Lo mejor para gastar."
            };
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id= id});

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id= regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object); 

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot,regionId, listIdCategories);

            touristSpotRepositoryMock.VerifyAll();
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
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void CreateInvalidTouristSpotWithoutDesc()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = ""
            };
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

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
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

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
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>())).Throws(new ExceptionRepository());
            touristSpotRepositoryMock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void CreateValidTouristSpotWithoutCategories()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar."
            };
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();

            var categoriesRepositoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(result.Id, touristSpot.Id);
        }

        [TestMethod]
        public void GetValidTouristSpotByRegion()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2, touristSpot3 }; 

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(listOfTouristSpotSearched);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotByRegion(region1.Id);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot1, result[0]); 
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidTouristSpotByRegion()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

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

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot); 

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotById(idForTouristSpot);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot, result); 

        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidTouristSpotById()
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

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotById(idForTouristSpot);
        }


        [TestMethod]
        public void GetValidTouristSpotSearchByCategories()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(listOfTouristSpotSearched);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id }; 

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesToSearch);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot1, result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidTouristSpotSearchByCategories()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesToSearch);
        }

        [TestMethod]
        public void GetValidTouristSpotSearchByCategoriesAndRegion()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(listOfTouristSpotSearched);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var result = touristSpotLogic.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot1, result[0]);
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidTouristSpotSearchByCategoriesAndRegion()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository()); 

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var result = touristSpotLogic.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id);
        }


        [TestMethod]
        public void UpdateValidTouristSpot()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "Colonia",
                Description = "Para tomarte un relax con tu pareja y descansar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            }; 

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2);
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save()); 

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);

            touristSpotRepositoryMock.VerifyAll();
            Assert.IsTrue(resultOfUpdate.Name.Equals("Colonia")); 

        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailInUpdateTouristSpot()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "Colonia",
                Description = "Para tomarte un relax con tu pareja y descansar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository());
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void FailInUpdateTouristSpotWithoutName()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "",
                Description = "Para tomarte un relax con tu pareja y descansar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void FailInUpdateTouristSpotWithoutDescription()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "Colonia",
                Description = "",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void FailInUpdateTouristSpotWithLargeDescription()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "Colonia",
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
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(TouristSpotException))]
        public void FailInUpdateTouristSpotWithoutCategories()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "Colonia",
                Description = "",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { }
            };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);
        }

        [TestMethod]
        public void RemoveValidTouristSpot()
        {
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot1);
            touristSpotRepositoryMock.Setup(m => m.Remove(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(new List<TouristSpot>()); 
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            touristSpotLogic.RemoveTouristSpot(touristSpot1.Id);

            List<TouristSpot> listOfTouristSpot = touristSpotLogic.GetAllTouristSpot(); 

            touristSpotRepositoryMock.VerifyAll();
            Assert.IsTrue(listOfTouristSpot.Count==0); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void RemoveInvalidTouristSpot()
        {
            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ExceptionRepository()); 
            touristSpotRepositoryMock.Setup(m => m.Remove(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(new List<TouristSpot>());
            touristSpotRepositoryMock.Setup(m => m.Save());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            touristSpotLogic.RemoveTouristSpot(touristSpot1.Id);
        }

        [TestMethod]
        public void GetAllTouristSpot()
        {
            List<TouristSpot> touristSpotToReturn = new List<TouristSpot>() { touristSpot1, touristSpot2, touristSpot3 }; 

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(touristSpotToReturn);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<TouristSpot> touristSpotObteinedOfGetAll = touristSpotLogic.GetAllTouristSpot(); 

            touristSpotRepositoryMock.VerifyAll();

            CollectionAssert.AreEqual(touristSpotToReturn, touristSpotObteinedOfGetAll); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void FailAboutGetAllTouristSpot()
        {
            List<TouristSpot> touristSpotToReturn = new List<TouristSpot>() { touristSpot1, touristSpot2, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository()); 

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<TouristSpot> touristSpotObteinedOfGetAll = touristSpotLogic.GetAllTouristSpot();
        }



    }
}
