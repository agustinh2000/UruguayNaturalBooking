using Castle.Core.Internal;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAcessTest
{
    [TestClass]
    public class UserDATest
    {
        User userToAdd;
        User userToAdd2;

        [TestInitialize]
        public void SetUp()
        {
            userToAdd = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };

            userToAdd2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                UserName = "joaco19",
                Mail = "joaco19@gmail.com",
                Password = "joaquinl19"
            };

        }
        [TestMethod]
        public void TestAddUserOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            userRepo.Add(userToAdd);
            List<User> listOfUsers = userRepo.GetAll().ToList();
            Assert.AreEqual(userToAdd, listOfUsers[0]);
        }

        [TestMethod]
        public void TestGetUserOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            userRepo.Add(userToAdd);
            User userOfDb = userRepo.Get(userToAdd.Id);
            Assert.AreEqual(userToAdd, userOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGetUserBad()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            userRepo.Get(userToAdd.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestRemoveUserOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            userRepo.Add(userToAdd);
            userRepo.Remove(userToAdd);
            userRepo.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestRemoveUserInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            User userToAdd = new User();
            userRepo.Remove(userToAdd);
        }

        [TestMethod]
        public void TestGetAllUsersOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            userRepo.Add(userToAdd);
            userRepo.Add(userToAdd2);
            List<User> listTest = new List<User>();
            listTest.Add(userToAdd);
            listTest.Add(userToAdd2);
            List<User> listOfUsers = userRepo.GetAll().ToList();
            CollectionAssert.AreEqual(listTest, listOfUsers);
        }

        [TestMethod]
        public void TestGetUserByEmailAndPasswordOk()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            userRepo.Add(userToAdd);
            User userObtained = userRepo.GetUserByEmailAndPassword("colo2020@gmail.com", "martin1234");
            Assert.AreEqual(userToAdd, userObtained);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGetUserByNicknameAndPasswordInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserRepository userRepo = new UserRepository(context);
            User userObtained = userRepo.GetUserByEmailAndPassword("colo2020@gmail.com", "martin1234");
        }
    }
}
