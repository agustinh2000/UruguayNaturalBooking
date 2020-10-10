using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class UserSessionRepository : BaseRepository<UserSession>, IUserSessionRepository
    {
        private readonly DbContext context;

        public UserSessionRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public UserSession GetUserSessionByUserId(Guid userId)
        {
            UserSession userSessionObteined = context.Set<UserSession>().Where(us => us.User.Id.Equals(userId)).FirstOrDefault();
            return userSessionObteined;
        }

        public UserSession GetUserSessionByToken(string token)
        {
            UserSession userSessionObteined = context.Set<UserSession>().Where(us => us.Token.Equals(token)).FirstOrDefault();
            return userSessionObteined;
        }
    }
}
