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
using System.Runtime.Intrinsics.X86;

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
        Picture picture;

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

            picture = new Picture()
            {
                Path = "Desktop/luis/foto.jpg"
            };

            touristSpot1 = new TouristSpot
            {
                Id = idForTouristSpot1,
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Region = region1,
                Image = picture,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1, categoryTouristSpot2 }
            };

            touristSpot2 = new TouristSpot
            {
                Id = idForTouristSpot2,
                Name = "Punta del diablo",
                Description = "Las mejores playas del Uruguay",
                Region = region2,
                Image = picture,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };

            touristSpot3 = new TouristSpot
            {
                Id = idForTouristSpot3,
                Name = "Salto",
                Description = "Las mejores naranjas del Uruguay y del mundo",
                Region = region2,
                Image = picture,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 }
            };
        }


        [TestMethod]
        public void CreateValidTouristSpot()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
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
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotWithoutPicture()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = null
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotEmptyName()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);
            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotWithoutDescription()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotExtendDescription()
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
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateInvalidTouristSpotWithErrorInAdd()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Donde la naturaleza y el lujo convergen",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>())).Throws(new ServerException());
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);

            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();
            listIdCategories.Add(id);

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotWithoutCategories()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void CreateInvalidTouristSpotWithoutRegion()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Throws(new ClientBusinessLogicException("Error en obtener la region."));

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotAlredyExist()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(touristSpot);


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateInvalidTouristSpotInternalErrorWhenSearchTouristSpotByName()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Throws(new ServerException());


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Returns(new Category() { Id = id });

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }


        [TestMethod]
        [ExpectedException(typeof(DomainBusinessLogicException))]
        public void CreateInvalidTouristSpotCategoriesAssociatedDoesntExist()
        {
            TouristSpot touristSpot = new TouristSpot
            {
                Id = Guid.NewGuid(),
                Name = "Punta del este",
                Description = "Lo mejor para gastar.",
                Image = picture
            };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByName(touristSpot.Name)).Returns(value: null);


            Guid regionId = Guid.NewGuid();
            List<Guid> listIdCategories = new List<Guid>();
            Guid id = Guid.NewGuid();

            var categoriesRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoriesRepositoryMock.Setup(m => m.Get(id)).Throws(new ClientBusinessLogicException());

            var regionsRepositoryMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionsRepositoryMock.Setup(m => m.Get(regionId)).Returns(new Region() { Id = regionId });

            var categoryLogic = new CategoryManagement(categoriesRepositoryMock.Object);
            var regionLogic = new RegionManagement(regionsRepositoryMock.Object);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object, regionLogic, categoryLogic);

            var result = touristSpotLogic.Create(touristSpot, regionId, listIdCategories);
        }

        [TestMethod]
        public void GetValidTouristSpotByRegion()
        {
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByRegion(region1.Id)).Returns(new List<TouristSpot>() { touristSpot1 });

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotByRegion(region1.Id);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot1, result[0]);
        }


        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetInvalidTouristSpotByRegionTestInternalError()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByRegion(It.IsAny<Guid>())).Throws(new ServerException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotByRegion(region1.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void GetInvalidTouristSpotByRegionTestNotFound()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotByRegion(It.IsAny<Guid>())).Throws(new ClientException());

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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotById(idForTouristSpot);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot, result);

        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetInvalidTouristSpotByIdServerErrorTest()
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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ServerException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotById(idForTouristSpot);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void GetInvalidTouristSpotByIdClientErrorTest()
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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotById(idForTouristSpot);
        }


        [TestMethod]
        public void GetValidTouristSpotSearchByCategories()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };

            List<Guid> listOfCategoriesIdToSearch = new List<Guid>() { category1.Id, category2.Id };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotsByCategories(listOfCategoriesIdToSearch)).Returns(new List<TouristSpot>() { touristSpot1 });

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesIdToSearch);

            touristSpotRepositoryMock.VerifyAll();
            Assert.IsTrue(new List<TouristSpot>(result).Contains(touristSpot1));
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetInvalidTouristSpotSearchByCategories()
        {
            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotsByCategories(listOfCategoriesToSearch)).Throws(new ServerException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesToSearch);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void GetNotFountTouristSpotSearchedByCategoriesTest()
        {
            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotsByCategories(listOfCategoriesToSearch)).Throws(new ClientException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotsByCategories(listOfCategoriesToSearch);
        }

        [TestMethod]
        public void GetValidTouristSpotSearchByCategoriesAndRegion()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1 };
            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id)).Returns(listOfTouristSpotSearched);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id);

            touristSpotRepositoryMock.VerifyAll();
            Assert.AreEqual(touristSpot1, result[0]);
        }


        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetInvalidTouristSpotSearchByCategoriesAndRegionInternalError()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };
            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id)).Throws(new ServerException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var result = touristSpotLogic.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void GetNotFoundTouristSpotSearchByCategoriesAndRegion()
        {
            List<TouristSpot> listOfTouristSpotSearched = new List<TouristSpot>() { touristSpot1, touristSpot2 };
            List<Guid> listOfCategoriesToSearch = new List<Guid>() { category1.Id, category2.Id };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesToSearch, region1.Id)).Throws(new ClientException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2);
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);

            touristSpotRepositoryMock.VerifyAll();
            Assert.IsTrue(resultOfUpdate.Name.Equals("Colonia"));

        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void FailInUpdateTouristSpot()
        {
            TouristSpot touristSpotToUpdate = new TouristSpot()
            {
                Id = touristSpot2.Id,
                Name = "Colonia",
                Description = "Para tomarte un relax con tu pareja y descansar.",
                Region = region1,
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ServerException());
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));

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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));

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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));

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
                ListOfCategories = new List<CategoryTouristSpot>() { categoryTouristSpot1 },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));

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
                ListOfCategories = new List<CategoryTouristSpot>() { },
                Image = picture
            };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot2); ;
            touristSpotRepositoryMock.Setup(m => m.Update(It.IsAny<TouristSpot>()));

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            var resultOfUpdate = touristSpotLogic.UpdateTouristSpot(touristSpotToUpdate);
        }

        [TestMethod]
        public void RemoveValidTouristSpot()
        {
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(touristSpot1);
            touristSpotRepositoryMock.Setup(m => m.Remove(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(new List<TouristSpot>());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            touristSpotLogic.RemoveTouristSpot(touristSpot1.Id);

            List<TouristSpot> listOfTouristSpot = touristSpotLogic.GetAllTouristSpot();

            touristSpotRepositoryMock.VerifyAll();
            Assert.IsTrue(listOfTouristSpot.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void RemoveInvalidTouristSpotInternalError()
        {
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ServerException());
            touristSpotRepositoryMock.Setup(m => m.Remove(It.IsAny<TouristSpot>()));
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(new List<TouristSpot>());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            touristSpotLogic.RemoveTouristSpot(touristSpot1.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void RemoveInvalidTouristSpotNotExist()
        {
            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ClientException());
            touristSpotRepositoryMock.Setup(m => m.Remove(It.IsAny<TouristSpot>()));

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            touristSpotLogic.RemoveTouristSpot(touristSpot1.Id);
        }

        [TestMethod]
        public void GetAllTouristSpot()
        {
            List<TouristSpot> touristSpotToReturn = new List<TouristSpot>() { touristSpot1, touristSpot2, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Returns(touristSpotToReturn);

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<TouristSpot> touristSpotObteinedOfGetAll = touristSpotLogic.GetAllTouristSpot();

            touristSpotRepositoryMock.VerifyAll();

            CollectionAssert.AreEqual(touristSpotToReturn, touristSpotObteinedOfGetAll);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void FailAboutGetAllTouristSpot()
        {
            List<TouristSpot> touristSpotToReturn = new List<TouristSpot>() { touristSpot1, touristSpot2, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Throws(new ServerException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<TouristSpot> touristSpotObteinedOfGetAll = touristSpotLogic.GetAllTouristSpot();
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void FailAboutGetAllTouristSpotClientExceptionTest()
        {
            List<TouristSpot> touristSpotToReturn = new List<TouristSpot>() { touristSpot1, touristSpot2, touristSpot3 };

            var touristSpotRepositoryMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            touristSpotRepositoryMock.Setup(m => m.GetAll()).Throws(new ClientException());

            var touristSpotLogic = new TouristSpotManagement(touristSpotRepositoryMock.Object);

            List<TouristSpot> touristSpotObteinedOfGetAll = touristSpotLogic.GetAllTouristSpot();
        }
    }
}
