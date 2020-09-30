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

        public User GetUserByNicknameAndPassword(string userName, string password)
        {
            try
            {
                User userObteined = context.Set<User>().Where(x => x.UserName.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
                if(userObteined == null)
                {
                    throw new ExceptionRepository("Usuario y/o contrasena incorrecta");
                }
                return userObteined;
            }
            catch (Exception e)
            {
                throw new ExceptionRepository(e.Message, e);
            } 
        }
    }
}
