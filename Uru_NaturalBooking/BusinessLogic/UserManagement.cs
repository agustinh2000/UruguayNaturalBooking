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
        private readonly IUserSessionRepository sessionRepository;

        public UserManagement(IUserRepository aUserRepository, IUserSessionRepository aSessionRepository)
        {
            userRepository = aUserRepository;
            sessionRepository = aSessionRepository;
        }

        public UserManagement(IUserRepository aUserRepository)
        {
            userRepository = aUserRepository;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return userRepository.GetAll();
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotExistUsers, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedAllUser, e);
            }

        }

        public bool IsLogued(string token)
        {
            UserSession userSession = sessionRepository.GetUserSessionByToken(token);
            return userSession != null;
        }

        public User Create(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                user.VerifyFormat();
                VerifyIfUserExist(user);
                userRepository.Add(user);
                return user;
            }
            catch (UserException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (DomainBusinessLogicException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear el usuario debido a que ha ocurrido un error.", e);
            }
        }

        private void VerifyIfUserExist(User user)
        {
            try
            {
                User userObteined = userRepository.GetUserByEmail(user.Mail);
                if (userObteined != null)
                {
                    throw new DomainBusinessLogicException(MessageExceptionBusinessLogic.ErrorUserAlredyExist);
                }
            }
            catch (ServerException e)
            {
                throw new ServerException("No se puede crear el usuario debido a que ha ocurrido un error.", e);
            }
        }

        public UserSession LogIn(string email, string password)
        {
            User user = null;
            try
            {
                user = userRepository.GetUserByEmailAndPassword(email, password);
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(e.Message);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(e.Message);
            }
            UserSession userSession = sessionRepository.GetUserSessionByUserId(user.Id);
            if (userSession == null)
            {
                Guid token = Guid.NewGuid();
                userSession = new UserSession()
                {
                    User = user,
                    Token = token.ToString()
                };
                sessionRepository.Add(userSession);
            }
            return userSession;
        }

        public User GetUser(Guid userId)
        {
            try
            {
                User userObteined = userRepository.Get(userId);
                return userObteined;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindUser, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedUser, e);
            }
        }

        public void LogOut(string token)
        {
            UserSession userSession = sessionRepository.GetUserSessionByToken(token);
            if (userSession == null)
            {
                throw new ClientBusinessLogicException(MessageExceptionDomain.ErrorTokenNotExist);
            }
            try
            {
                sessionRepository.Remove(userSession);
            }
            catch (ServerException)
            {
                throw new ServerBusinessLogicException("No es posible cerrar la sesion.");

            }
        }

        public User UpdateUser(Guid userToModifyId, User aUser)
        {
            try
            {
                User userDb = GetUser(userToModifyId);
                userDb.UpdateAttributes(aUser);
                userDb.VerifyFormat();
                userRepository.Update(userDb);
                return userDb;
            }
            catch (UserException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ClientBusinessLogicException e)
            {
                throw new ClientBusinessLogicException(e.Message);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede actualizar el usuario.", e);
            }
        }

        public void RemoveUser(Guid userId)
        {
            try
            {
                User userToDelete = userRepository.Get(userId);
                userRepository.Remove(userToDelete);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede eliminar el usuario deseado.", e);
            }
        }
    }
}
