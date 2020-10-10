using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ForResponseAndRequest;
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
        CategoryModel categoryBeachModel;
        CategoryModel categoryAtractionsModel; 

        [TestInitialize]
        public void SetUp()
        {
             categoryBeachModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

             categoryAtractionsModel = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = "Atracciones"
            };
        }

        [TestMethod]
        public void GetCategoryTestOk()
        {

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(categoryBeachModel.ToEntity());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get(categoryBeachModel.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as CategoryModel;

            mock.VerifyAll();
            Assert.AreEqual(categoryBeachModel.Name, model.Name); 
            Assert.AreEqual(categoryBeachModel.Id, model.Id);
        }

        [TestMethod]
        
        public void GetCategoryTestFailed()
        {
            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ClientBusinessLogicException());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get(categoryBeachModel.Id);
            var createdResult = result as NotFoundObjectResult;
            mock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]

        public void GetCategoryTestInternalServer()
        {
            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("No se pudo obtener la categoria deseada."));
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get(categoryBeachModel.Id);
            var createdResult = result as ObjectResult;
            mock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]

        public void GetAllCategoriesOkTest()
        {
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

        public void GetAllCategoriesNotFoundTest()
        {
            List<CategoryModel> listOfCategoriesModel = new List<CategoryModel>() { categoryBeachModel, categoryAtractionsModel };
            List<Category> listOfCategories = new List<Category>() { categoryBeachModel.ToEntity(), categoryAtractionsModel.ToEntity() };

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllCategories()).Throws(new ClientBusinessLogicException());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get();
            var createdResult = result as NotFoundObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode); 
        }

        [TestMethod]
        public void GetAllCategoriesInternalErrorTest()
        {
            List<CategoryModel> listOfCategoriesModel = new List<CategoryModel>() { categoryBeachModel, categoryAtractionsModel };
            List<Category> listOfCategories = new List<Category>() { categoryBeachModel.ToEntity(), categoryAtractionsModel.ToEntity() };

            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllCategories()).Throws(new ServerBusinessLogicException("No se ha podido obtener las regiones"));
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Get();
            var createdResult = result as ObjectResult;

            mock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void PostACategoryTest()
        {
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
        public void TestPostCategoryWithInternalServerError()
        {
            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Category>())).Throws(new ServerBusinessLogicException());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Post(categoryBeachModel);
            var createdResult = result as ObjectResult;
            mock.VerifyAll(); 
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void TestPostCategoryWithDomainErrorError()
        {
            var mock = new Mock<ICategoryManagement>(MockBehavior.Strict);
            mock.Setup(m => m.Create(It.IsAny<Category>())).Throws(new DomainBusinessLogicException());
            CategoryController categoryController = new CategoryController(mock.Object);

            var result = categoryController.Post(categoryBeachModel);
            var createdResult = result as BadRequestObjectResult;
            mock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

    }
}
