using System;

namespace RepositoryException
{
    public class ExceptionRepository: Exception
    {
        public ExceptionRepository() : base() { }


        public ExceptionRepository(String message) : base(message) { }


        public ExceptionRepository(String message, Exception exception) : base(message, exception) { }
    }
}
