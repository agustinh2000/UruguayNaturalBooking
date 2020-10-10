using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface IUserSessionRepository: IRepository<UserSession>
    {
        UserSession GetUserSessionByUserId(Guid userId);
        UserSession GetUserSessionByToken(string token);
    }
}
