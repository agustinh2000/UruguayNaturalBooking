using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class UserException: Exception
    {
        public UserException() : base() { }


        public UserException(String message) : base(message) { }


        public UserException(String message, Exception exception) : base(message, exception) { }
    }
}
