using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Security;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class CategoryControllerTest
    {
        [TestMethod]
        public void GetCategoryTestOk()
        {
            CategoryModel categoryModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(categoryModel.ToEntity());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get(categoryModel.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as CategoryModel;

            mock.VerifyAll();
            Assert.AreEqual(categoryModel.Name, model.Name); 
            Assert.AreEqual(categoryModel.Id, model.Id);
        }

        [TestMethod]
        
        public void GetCategoryTestFailed()
        {
            CategoryModel categoryModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(value: null);
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get(categoryModel.Id);
            var createdResult = result as NotFoundObjectResult;
            var resultValue = createdResult.Value;
            mock.VerifyAll();
            Assert.AreEqual("El objeto solicitado no fue encontrado", resultValue);
        }

        [TestMethod]

        public void GetAllCategoriesOk()
        {
            CategoryModel categoryBeachModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            CategoryModel categoryAtractionsModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Atracciones"
            };

            List<CategoryModel> listOfCategoriesModel = new List<CategoryModel>() { categoryBeachModel, categoryAtractionsModel };
            List<Category> listOfCategories = new List<Category>() { categoryBeachModel.ToEntity(), categoryAtractionsModel.ToEntity() }; 

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllCategories()).Returns(listOfCategories) ;
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get();
            var createdResult = result as OkObjectResult;
            var resultValue = createdResult.Value as List<CategoryModel>;


            mock.VerifyAll();
            CollectionAssert.AreEqual(listOfCategoriesModel, resultValue);
        }

        [TestMethod]
        public void PostACategory()
        {
            CategoryModel categoryBeachModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Category categoryToReturn = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            }; 

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Category>())).Returns(categoryToReturn);
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Post(categoryBeachModel);
            var createdResult = result as CreatedAtRouteResult;
            var resultValue = createdResult.Value as CategoryModel;

            mock.VerifyAll();
            Assert.AreEqual(categoryBeachModel, resultValue); 
        }

        [TestMethod]
        public void TestPostMovieIncorrectCategory()
        {

            CategoryModel categoryBeachModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Category>())).Throws(new ArgumentException());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Post(categoryBeachModel);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

    }
}
