using Domain;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Model.ForResponse
{
    public class UserModelForResponse : ModelBaseForResponse<User, UserModelForResponse>
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        protected override UserModelForResponse SetModel(User entity)
        {
            Id = entity.Id;
            UserName = entity.UserName;
            Mail = entity.Mail;
            Name = entity.Name;
            LastName = entity.LastName;
            Password = entity.Password;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is UserModelForResponse response &&
                   UserName.Equals(response.UserName) &&
                   Mail.Equals(response.Mail);
        }
    }

}

