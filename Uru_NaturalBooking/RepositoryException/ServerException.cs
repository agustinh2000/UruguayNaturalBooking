using System;

namespace RepositoryException
{
    public class ServerException: Exception
    {
        public ServerException() : base() { }


        public ServerException(String message) : base(message) { }


        public ServerException(String message, Exception exception) : base(message, exception) { }
    }
}
