using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbContext context;
        
        public UserRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                User userObteined = context.Set<User>().Where(x => x.Mail.Equals(email) && x.Password.Equals(password)).FirstOrDefault();
                if(userObteined == null)
                {
                    throw new ServerException("Email y/o contrasena incorrecta");
                }
                return userObteined;
            }
            catch (Exception e)
            {
                throw new ServerException(e.Message, e);
            } 
        }
    }
}
