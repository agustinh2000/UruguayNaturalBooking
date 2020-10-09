using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryException
{
    public class ClientException : Exception
    {
        public ClientException() : base() { }


        public ClientException(String message) : base(message) { }


        public ClientException(String message, Exception exception) : base(message, exception) { }
    }
}
