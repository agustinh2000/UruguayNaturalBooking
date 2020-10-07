using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IUserManagement
    {
        User Create(User user);
        IEnumerable<User> GetAll();
        UserSession LogIn(string email, string password);
        void LogOut(string token);
        bool IsLogued(string token);
        User UpdateUser(Guid userToModifyId, User aUser);
        void RemoveUser(Guid userId);
        User GetUser(Guid userId);
    }
}
