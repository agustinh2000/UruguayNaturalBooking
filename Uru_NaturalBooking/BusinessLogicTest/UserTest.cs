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
    public class UserTest
    {
        [TestMethod]
        public void CreateValidUser()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(result.Id, user.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]

        public void CreateInvalidUserRepositoryException()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>())).Throws(new ExceptionRepository());
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
        }


        [TestMethod]
        [ExpectedException(typeof(UserException))]
        public void CreateInvalidUserEmptyName()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserException))]
        public void CreateInvalidUserEmptyLastName()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserException))]
        public void CreateInvalidUserEmptyUserName()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserException))]
        public void CreateInvalidUserNotValidMail()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@.com",
                Password = "martin1234"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserException))]
        public void CreateInvalidUserEmptyPassword()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@.com",
                Password = ""
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.Create(user);
        }

        [TestMethod]
        public void GetAllUsersOk()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "soyelcolobienfachero"
            };
            User user2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Martinez",
                UserName = "joac123",
                Mail = "joaq1239@hotmail.com",
                Password = "joaco12345"
            };
            List<User> expectedResult = new List<User>() { user, user2 };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            userRepositoryMock.Setup(m => m.GetAll()).Returns(expectedResult);
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            User userAdded = userLogic.Create(user);
            User userAdded2 = userLogic.Create(user2);
            List<User> obteinedResult = userLogic.GetAll().ToList();
            userRepositoryMock.VerifyAll();
            CollectionAssert.AreEqual(expectedResult, obteinedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]

        public void GetAllUsersInvalid()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "soyelcolobienfachero"
            };
           
            List<User> expectedResult = new List<User>() { user};
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.Save());
            userRepositoryMock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.GetAll().ToList();
        }

        [TestMethod]
        public void LogInOk()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            User user2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo20201@gmail.com",
                Password = "colo123"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.GetUserByNicknameAndPassword("colo20", "colo123")).Returns(user); ;
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>() );
            userSessionRepositoryMock.Setup(m => m.Add(It.IsAny<UserSession>()));
            userSessionRepositoryMock.Setup(m => m.Save());
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>());
            var userSessionRepositoryMock2 = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock2.Setup(m => m.GetAll()).Returns(new List<UserSession>() { new UserSession() { User = user} }) ;
            userSessionRepositoryMock2.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.Create(user);
            userLogic.LogIn("colo20", "colo123");
            var userSessions = userSessionRepositoryMock2.Object.GetAll();
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, userSessions.First().User);
        }

        [TestMethod]
        public void LogInOk2()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            User user2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo20201@gmail.com",
                Password = "colo123"
            };

            UserSession userSession = new UserSession()
            {
                User = user2
            };
            
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.GetUserByNicknameAndPassword("colo20", "colo123")).Returns(user); ;
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>() { userSession});
            userSessionRepositoryMock.Setup(m => m.Add(It.IsAny<UserSession>()));
            userSessionRepositoryMock.Setup(m => m.Save());
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>());
            var userSessionRepositoryMock2 = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock2.Setup(m => m.GetAll()).Returns(new List<UserSession>() { new UserSession() { User = user } });
            userSessionRepositoryMock2.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.Create(user);
            userLogic.LogIn("colo20", "colo123");
            var userSessions = userSessionRepositoryMock2.Object.GetAll();
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, userSessions.First().User);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void LogInFailedPassIncorrect()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.GetUserByNicknameAndPassword("colo20", "colo123")).Throws(new ExceptionRepository()); ;
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.LogIn("colo20", "colo123");
        }

        [TestMethod]
        public void LogInOkUserSessionAlreadyExist()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            UserSession userSession = new UserSession()
            {
                User = user
            };
            
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(m => m.Add(It.IsAny<User>()));
            userRepositoryMock.Setup(m => m.GetUserByNicknameAndPassword("colo20", "colo123")).Returns(user); ;
            userRepositoryMock.Setup(m => m.Save());
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>() { userSession});
            userSessionRepositoryMock.Setup(m => m.Add(It.IsAny<UserSession>()));
            userSessionRepositoryMock.Setup(m => m.Save());
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.Create(user);
            var session = userLogic.LogIn("colo20", "colo123");
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(userSession.User, session.User);
        }

        [TestMethod]
        public void LogOutOk()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            string aToken = Guid.NewGuid().ToString();

            UserSession userSession = new UserSession()
            {
                User = user,
                Token = aToken
            };

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>() { userSession });
            userSessionRepositoryMock.Setup(m => m.Remove(It.IsAny<UserSession>()));
            userSessionRepositoryMock.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.LogOut(aToken);
            userRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void LogOutFailedWhenGetSessions()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            string aToken = Guid.NewGuid().ToString();

            UserSession userSession = new UserSession()
            {
                User = user,
                Token = aToken
            };

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession> ());
            userSessionRepositoryMock.Setup(m => m.Remove(It.IsAny<UserSession>()));
            userSessionRepositoryMock.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.LogOut(aToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void LogOutFailedWhenRemoveSession()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            string aToken = Guid.NewGuid().ToString();

            UserSession userSession = new UserSession()
            {
                User = user,
                Token = aToken
            };

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>() { userSession });
            userSessionRepositoryMock.Setup(m => m.Remove(It.IsAny<UserSession>())).Throws(new ExceptionRepository());
            userSessionRepositoryMock.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            userLogic.LogOut(aToken);
        }

        [TestMethod]
        public void UserIsLogued()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };

            string aToken = Guid.NewGuid().ToString();

            UserSession userSession = new UserSession()
            {
                User = user,
                Token = aToken
            };

            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>() { userSession });
            userSessionRepositoryMock.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.IsLogued(aToken);
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserIsNotLogued()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "colo123"
            };
            string aToken = Guid.NewGuid().ToString();
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var userSessionRepositoryMock = new Mock<IRepository<UserSession>>(MockBehavior.Strict);
            userSessionRepositoryMock.Setup(m => m.GetAll()).Returns(new List<UserSession>());
            userSessionRepositoryMock.Setup(m => m.Save());
            var userLogic = new UserManagement(userRepositoryMock.Object, userSessionRepositoryMock.Object);
            var result = userLogic.IsLogued(aToken);
            userRepositoryMock.VerifyAll();
            Assert.IsFalse(result);
        }












    }
}
