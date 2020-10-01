using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository userRepository;
        private readonly IRepository<UserSession> sessionRepository;

        public UserManagement(IUserRepository aUserRepository, IRepository<UserSession> aSessionRepository)
        {
            userRepository = aUserRepository;
            sessionRepository = aSessionRepository;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return userRepository.GetAll();
            }
            catch (ExceptionRepository)
            {
                throw new ExceptionBusinessLogic("No se pueden obtener los usuarios.");
            }
            
        }

        public bool IsLogued(string token)
        {
            UserSession userSession = sessionRepository.GetAll().Where(x => x.Token == token).FirstOrDefault();
            return userSession != null;
        }

        public User Create(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                user.VerifyFormat();
                userRepository.Add(user);
                userRepository.Save();
                return user;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede crear el usuario debido a que ha ocurrido un error.", e);
            }
        }

        public UserSession LogIn(string nickname, string password)
        {
            User user = null;
            try
            {
                user = userRepository.GetUserByNicknameAndPassword(nickname, password);
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("Usuario y/o password incorrecto");
            }
            UserSession userSession = sessionRepository.GetAll().Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
            if (userSession == null)
            {
                Guid token = Guid.NewGuid();
                userSession = new UserSession()
                {
                    User = user,
                    Token = token.ToString()
                };
                sessionRepository.Add(userSession);
                sessionRepository.Save();
            }
            return userSession;
        }

        public void LogOut(string token)
        {
            UserSession userSession = sessionRepository.GetAll().Where(x => x.Token == token).FirstOrDefault();
            if (userSession == null)
            {
                throw new ExceptionBusinessLogic(MessageException.ErrorTokenNotExist);
            }
            try
            {
                sessionRepository.Remove(userSession);
            }
            catch (ExceptionRepository)
            {
                throw new ExceptionBusinessLogic("No es posible cerrar la sesion.");

            }
            sessionRepository.Save();
        }
    }
}
