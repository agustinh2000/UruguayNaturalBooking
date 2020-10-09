
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
    public class CategoryTest
    {
        [TestMethod]
        public void GetCategoryByIdTestOk()
        {
            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Name= "Playita y calor"
            };
            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(category);

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);
            Category result = categoryLogic.GetById(category.Id);

            Category categoryToCompare = new Category()
            {
                Id = result.Id,
                Name = result.Name
            }; 

            categoryMock.VerifyAll();
            Assert.AreEqual(category, categoryToCompare);
        }

        [TestMethod]
        public void GetNullCategoryByIdTest()
        {
            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Playita y calor"
            };
            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(value: null);

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);
            Category result = categoryLogic.GetById(category.Id);

            categoryMock.VerifyAll();
            Assert.IsFalse(category.Equals(result)); 
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetInvalidCategoryByIdTest()
        {
            Guid idForCategory = Guid.NewGuid();
            Category category = new Category
            {
                Id = idForCategory, 
                Name = "Playita y calor"
            };

            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);

            categoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Throws(new ServerException());

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            categoryLogic.GetById(idForCategory);
        }

        [TestMethod]
        public void GetAllCategoriesTest()
        {

            List<Category> listOfCategories = new List<Category>();
            Category categoryOfBeach = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Playita y calor"
            };
            listOfCategories.Add(categoryOfBeach);
            Category categoryOfCold = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Frio"
            };
            listOfCategories.Add(categoryOfCold);

            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.GetAll()).Returns(new List<Category>(listOfCategories));

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            List<Category> result = categoryLogic.GetAllCategories();

            categoryMock.VerifyAll();
            CollectionAssert.AreEqual(listOfCategories, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetExceptionObteinedAllCategoriesTest()
        {

            List<Category> listOfCategories = new List<Category>();
            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Playita y calor"
            };
            listOfCategories.Add(category);
            Category category2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Frio"
            };
            listOfCategories.Add(category2);


            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.GetAll()).Throws(new ServerException());

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            List<Category> result = categoryLogic.GetAllCategories();
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void GetClientExceptionObteinedAllCategoriesTest()
        {

            List<Category> listOfCategories = new List<Category>();
            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Playita y calor"
            };
            listOfCategories.Add(category);
            Category category2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Frio"
            };
            listOfCategories.Add(category2);


            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.GetAll()).Throws(new ClientException());

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            List<Category> result = categoryLogic.GetAllCategories();
        }


        [TestMethod]
        public void GetAllCategoriesAssociatedTestOk()
        {

            List<Guid> listOfIdentifier = new List<Guid>();
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid(); 
            listOfIdentifier.Add(guid1);
            listOfIdentifier.Add(guid2); 


            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Get(guid1)).Returns(new Category() { Id = guid1 });
            categoryMock.Setup(m => m.Get(guid2)).Returns(new Category() { Id= guid2}); 

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            List<Category> result = categoryLogic.GetAssociatedCategories(listOfIdentifier);

            List<Guid> listOfIdentifiersGetted = new List<Guid>(); 
            foreach(Category category in result)
            {
                listOfIdentifiersGetted.Add(category.Id); 
            }

            categoryMock.VerifyAll();
            CollectionAssert.AreEqual(listOfIdentifier, listOfIdentifiersGetted); 
        }


        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetExceptionGettingAllCategoriesAssociatedTest()
        {

            List<Guid> listOfIdentifier = new List<Guid>();
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            listOfIdentifier.Add(guid1);
            listOfIdentifier.Add(guid2);

            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new Category() { Id = guid1 });
            categoryMock.Setup(m => m.Get(It.IsAny<Guid>())).Throws(new ServerException());

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            List<Category> result = categoryLogic.GetAssociatedCategories(listOfIdentifier);
        }


        [TestMethod]
        public void CreateValidCategoryTestOk()
        {
            Category category = new Category
            {
                Name = "Playita y calor"
            };

            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Add(It.IsAny<Category>()));

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            Category result = categoryLogic.Create(category);

            categoryMock.VerifyAll();
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void CreateInvalidCategory()
        {
            Category category = new Category
            {
                Name = "Playita y calor"
            };

            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Add(It.IsAny<Category>())).Throws(new ServerException());

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            Category result = categoryLogic.Create(category);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoryException))]
        public void CreateInvalidCategoryWithoutName()
        {
            Category category = new Category
            {
                Name = ""
            };

            var categoryMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            categoryMock.Setup(m => m.Add(It.IsAny<Category>()));

            CategoryManagement categoryLogic = new CategoryManagement(categoryMock.Object);

            Category result = categoryLogic.Create(category);
        }

    }
}
