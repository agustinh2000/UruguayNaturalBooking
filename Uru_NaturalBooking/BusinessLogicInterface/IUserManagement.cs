using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IUserManagement
    {
        IEnumerable<User> GetAll();
        UserSession LogIn(string nickname, string password);
        void LogOut(string token);
        bool IsLogued(string token);
    }
}
