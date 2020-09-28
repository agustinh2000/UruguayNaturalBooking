using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class LodgingException : Exception
    {
        public LodgingException() : base() { }

        public LodgingException(String message) : base(message) { }

        public LodgingException(String message, Exception exception) : base(message, exception) { }
    }
}
