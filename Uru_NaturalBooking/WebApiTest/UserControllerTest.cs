using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ForResponse;
using Moq;
using System;
using System.Collections.Generic;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class UserControllerTest
    {
        UserSession aUserSession;

        LoginModel aLoginModel;

        User aUser;

        UserModelForResponse aUserModifiedModel;

        UserModelForResponse aUserModel;

        User aUserModified;

        User invalidUser;

        [TestInitialize]
        public void SetUp()
        {

            aUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Agustin",
                LastName = "Hernandorena",
                Mail = "agustinhernandorena@gmail.com",
                Password = "agustinh123",
                UserName = "agustinh2000"
            };

            aUserModified = new User()
            {
                Id = aUser.Id,
                Name = "Pablo",
                LastName = "Hernandorena",
                Mail = "agustinhernandorena2020@gmail.com",
                Password = "agustinh123",
                UserName = "agustinh2000"
            };

            invalidUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = "",
                LastName = "Hernandorena",
                Mail = "agustinhernandorena@gmail.com",
                Password = "agustinh123",
                UserName = "agustinh2000"
            };

            aUserModifiedModel = new UserModelForResponse()
            {
                Mail = "agustinhernandorena2020@gmail.com",
                UserName = "agustinh2000"
            };

            aUserModel = new UserModelForResponse()
            {
                Mail = "agustinhernandorena@gmail.com",
                UserName = "agustinh2000"
            };

            aLoginModel = new LoginModel()
            {
                Email = "agustinhernandorena@gmail.com",
                Password = "agustinh123"
            };

            aUserSession = new UserSession()
            {
                Id = Guid.NewGuid(),
                Token = Guid.NewGuid().ToString(),
                User = aUser
            };

        }

        [TestMethod]
        public void LogInOkTest()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.LogIn(aLoginModel.Email, aLoginModel.Password)).Returns(aUserSession);
            UserController userController = new UserController(userMock.Object);
            var result = userController.Login(aLoginModel);
            var createdResult = result as OkObjectResult;
            var userModelResult = createdResult.Value as UserModelForResponse;
            userMock.VerifyAll();
            Assert.AreEqual(aUserModel, userModelResult);
        }

        [TestMethod]
        public void LogInUserAndPasswordIncorrectTest()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.LogIn(aLoginModel.Email, aLoginModel.Password)).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Login(aLoginModel);
            var errorResult = result as BadRequestObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(400, errorResult.StatusCode);
        }

        [TestMethod]
        public void PostUserOk()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.Create(aUser)).Returns(aUser);
            UserController userController = new UserController(userMock.Object);
            var result = userController.Post(aUser);
            var createdResult = result as CreatedAtRouteResult;
            var userResult = createdResult.Value as UserModelForResponse;
            userMock.VerifyAll();
            Assert.AreEqual(aUserModel, userResult);
        }

        [TestMethod]
        public void PostUserInvalidInternalServerError()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.Create(invalidUser)).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Post(invalidUser);
            var errorResult = result as ObjectResult;
            Assert.AreEqual(500, errorResult.StatusCode);
        }

        [TestMethod]
        public void PostUserInvalidInformation()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.Create(invalidUser)).Throws(new DomainBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Post(invalidUser);
            var errorResult = result as BadRequestObjectResult;
            Assert.AreEqual(400, errorResult.StatusCode);
        }

        [TestMethod]
        public void PutUserOk()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.UpdateUser(aUser.Id, aUserModified)).Returns(aUserModified);
            UserController userController = new UserController(userMock.Object);
            var result = userController.Put(aUser.Id, aUserModified);
            var createdResult = result as CreatedAtRouteResult;
            var userModelResult = createdResult.Value as UserModelForResponse;
            userMock.VerifyAll();
            Assert.AreEqual(aUserModifiedModel, userModelResult);
        }

        [TestMethod]
        public void GetAllUsersOk()
        {
            List<User> userList = new List<User> { aUser };
            List<UserModelForResponse> userModelList = new List<UserModelForResponse> { aUserModel };
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.GetAll()).Returns(userList);
            UserController userController = new UserController(userMock.Object);
            var result = userController.Get();
            var createdResult = result as OkObjectResult;
            var userModelResult = createdResult.Value as List<UserModelForResponse>;
            userMock.VerifyAll();
            CollectionAssert.AreEqual(userModelList, userModelResult);
        }

        [TestMethod]
        public void GetAllUsersNotFound()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.GetAll()).Throws(new ClientBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Get();
            var errorResult = result as NotFoundObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(404, errorResult.StatusCode);
        }

        [TestMethod]
        public void GetAllUsersInternalError()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.GetAll()).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Get();
            var errorResult = result as ObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(500, errorResult.StatusCode);
        }

        [TestMethod]
        public void GetUserByIdOk()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.GetUser(aUser.Id)).Returns(aUser);
            UserController userController = new UserController(userMock.Object);
            var result = userController.Get(aUser.Id);
            var createdResult = result as OkObjectResult;
            var userModelResult = createdResult.Value as UserModelForResponse;
            userMock.VerifyAll();
            Assert.AreEqual(aUserModel, userModelResult);
        }

        [TestMethod]
        public void GetUserByIdNotFound()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.GetUser(aUser.Id)).Throws(new ClientBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Get(aUser.Id);
            var createdResult = result as NotFoundObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetUserByIdInternalError()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.GetUser(aUser.Id)).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Get(aUser.Id);
            var createdResult = result as ObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void PutUserNotExist()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.UpdateUser(aUser.Id, aUserModified)).Throws(new ClientBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Put(aUser.Id, aUserModified);
            var createdResult = result as NotFoundObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void PutUserInternalErrorTest()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.UpdateUser(aUser.Id, aUserModified)).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Put(aUser.Id, aUserModified);
            var createdResult = result as ObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void PutUserWithAnyErrorFieldTest()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.UpdateUser(aUser.Id, aUserModified)).Throws(new DomainBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Put(aUser.Id, aUserModified);
            var createdResult = result as BadRequestObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void DeleteUserOk()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.RemoveUser(aUser.Id));
            UserController userController = new UserController(userMock.Object);
            var result = userController.Delete(aUser.Id);
            var createdResult = result as NoContentResult;
            userMock.VerifyAll();
            Assert.AreEqual(204, createdResult.StatusCode);
        }

        [TestMethod]
        public void DeleteUserInternalError()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.RemoveUser(aUser.Id)).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Delete(aUser.Id);
            var createdResult = result as ObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void LogOutOk()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.LogOut(aUserSession.Token));
            UserController userController = new UserController(userMock.Object);
            var result = userController.Logout(aUserSession.Token);
            var createdResult = result as OkResult;
            userMock.VerifyAll();
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        [TestMethod]
        public void LogOutInvalidToken()
        {
            var userMock = new Mock<IUserManagement>(MockBehavior.Strict);
            userMock.Setup(m => m.LogOut(aUserSession.Token)).Throws(new ServerBusinessLogicException());
            UserController userController = new UserController(userMock.Object);
            var result = userController.Logout(aUserSession.Token);
            var createdResult = result as NotFoundObjectResult;
            userMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }







    }
}
