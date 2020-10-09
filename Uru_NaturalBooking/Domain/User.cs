using DomainException;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public void VerifyFormat()
        {
            if (IsAtLeastOneEmptyField())
            {
                throw new UserException(MessageExceptionDomain.ErrorIsEmpty);
            }
            if (!IsValidEmail())
            {
                throw new UserException(MessageExceptionDomain.ErrorInvalidEmail);
            }
        }

        private bool IsAtLeastOneEmptyField()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(LastName) ||
                String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password) ||
                String.IsNullOrEmpty(Mail);
        }

        private bool IsValidEmail()
        {
            try
            {
                MailAddress address = new System.Net.Mail.MailAddress(Mail);
                return address.Address.Equals(Mail);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void UpdateAttributes(User aUser)
        {
            Name = aUser.Name;
            LastName = aUser.LastName;
            UserName = aUser.UserName;
            Password = aUser.Password;
            Mail = aUser.Mail;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Mail == user.Mail;
        }
    }

}
