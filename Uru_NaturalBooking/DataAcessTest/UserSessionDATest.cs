﻿using Castle.Core.Internal;
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
    public class UserSessionDATest
    {
        User user;
        User user2;
        UserSession userSession;
        UserSession userSession2;


        [TestInitialize]
        public void SetUp()
        {
            user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                LastName = "Gutman",
                UserName = "colo20",
                Mail = "colo2020@gmail.com",
                Password = "martin1234"
            };

            user2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Joaquin",
                LastName = "Lamela",
                UserName = "joaco19",
                Mail = "joaco19@gmail.com",
                Password = "joaquinl19"
            };

            userSession = new UserSession()
            {
                Id = Guid.NewGuid(),
                User = user,
                Token = Guid.NewGuid().ToString()
            };

            userSession2 = new UserSession()
            {
                Id = Guid.NewGuid(),
                User = user2,
                Token = Guid.NewGuid().ToString()
            };


        }
        
        [TestMethod]
        public void TestAddUserSessionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Add(userSession);
            List<UserSession> listOfUserSessions = userSessionRepo.GetAll().ToList();
            Assert.AreEqual(userSession, listOfUserSessions[0]);
        }

        [TestMethod]
        public void TestGetUserSessionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Add(userSession);
            UserSession userSessionOfDb = userSessionRepo.Get(userSession.Id);
            Assert.AreEqual(userSession, userSessionOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestGetUserSessionBad()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Get(userSession.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientException))]
        public void TestRemoveUserSessionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Add(userSession);
            userSessionRepo.Remove(userSession);
            userSessionRepo.GetAll(); 
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestRemoveUserSessionInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Remove(userSession);
        }

        [TestMethod]
        public void TestGetUserSessionByUserId()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Add(userSession);
            UserSession userSessionResult = userSessionRepo.GetUserSessionByUserId(user.Id);
            Assert.AreEqual(user, userSessionResult.User);
        }

        [TestMethod]
        public void TestGetUserSessionByToken()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Add(userSession);
            UserSession userSessionResult = userSessionRepo.GetUserSessionByToken(userSession.Token);
            Assert.AreEqual(user, userSessionResult.User);
        }


        [TestMethod]
        public void TestGetAllUsersSessionsOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IUserSessionRepository userSessionRepo = new UserSessionRepository(context);
            userSessionRepo.Add(userSession);
            userSessionRepo.Add(userSession2);
            List<UserSession> listTest = new List<UserSession>();
            listTest.Add(userSession);
            listTest.Add(userSession2);
            List<UserSession> listOfUserSessions = userSessionRepo.GetAll().ToList();
            CollectionAssert.AreEqual(listTest, listOfUserSessions);
        }
    }
}
