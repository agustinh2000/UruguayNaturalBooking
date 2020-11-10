using System;

namespace ImporterException
{
    public class ImportationException : Exception
    {
        public ImportationException(String message) : base(message) { }
        public ImportationException(String message, Exception exception) : base(message, exception) { }
    }
}
