using System;

namespace BusinessLogicException
{
    public class ServerBusinessLogicException : Exception
    {
        public ServerBusinessLogicException() : base() { }

        public ServerBusinessLogicException(String message) : base(message) { }

        public ServerBusinessLogicException(String message, Exception exception) : base(message, exception) { }
    }
}
