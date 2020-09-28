
using BusinessLogic;
using BusinessLogicException;
using DataAccessInterface;
using Domain;
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
        public void GetCategoryById()
        {
            Category category = new Category
            {
                Id = Guid.NewGuid(),
                Name= "Playita y calor"
            };
            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(category.Id)).Returns(category);

            var categoryLogic = new CategoryManagement(mock.Object);

            var result = categoryLogic.GetById(category.Id);

            mock.VerifyAll();
            Assert.AreEqual(category.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidCategoryById()
        {
            var guid = Guid.NewGuid();
            Category category = new Category
            {
                Id = guid, 
                Name = "Playita y calor"
            };

            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);

            mock.Setup(x => x.Get(guid)).Throws(new ExceptionRepository());

            var categoryLogic = new CategoryManagement(mock.Object);

            categoryLogic.GetById(guid);
        }

        [TestMethod]
        public void GetAllCategories()
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


            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(listOfCategories);

            var categoryLogic = new CategoryManagement(mock.Object);

            var result = categoryLogic.GetAllCategories();

            mock.VerifyAll();
            CollectionAssert.AreEqual(listOfCategories, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetBadAllCategories()
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


            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());

            var categoriLogic = new CategoryManagement(mock.Object);

            var result = categoriLogic.GetAllCategories();

        }


        [TestMethod]
        public void GetAllCategoriesAssociated()
        {

            List<Guid> listOfIdentifier = new List<Guid>();
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid(); 
            listOfIdentifier.Add(guid1);
            listOfIdentifier.Add(guid2); 


            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid1)).Returns(new Category() { Id = guid1 });
            mock.Setup(m => m.Get(guid2)).Returns(new Category() { Id= guid2}); 

            var categoryLogic = new CategoryManagement(mock.Object);

            var result = categoryLogic.GetAssociatedCategories(listOfIdentifier);

            List<Guid> listOfIdentifiersGetted = new List<Guid>(); 
            foreach(Category category in result)
            {
                listOfIdentifiersGetted.Add(category.Id); 
            }

            mock.VerifyAll();
            CollectionAssert.AreEqual(listOfIdentifier, listOfIdentifiersGetted); 
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetBadAllCategoriesAssociated()
        {

            List<Guid> listOfIdentifier = new List<Guid>();
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            listOfIdentifier.Add(guid1);
            listOfIdentifier.Add(guid2);


            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid1)).Returns(new Category() { Id = guid1 });
            mock.Setup(m => m.Get(guid2)).Throws(new ExceptionRepository());

            var categoryLogic = new CategoryManagement(mock.Object);

            var result = categoryLogic.GetAssociatedCategories(listOfIdentifier);

        }

    }
}
