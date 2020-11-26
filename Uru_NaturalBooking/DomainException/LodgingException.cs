using System;

namespace DomainException
{
    public class LodgingException : Exception
    {
        public LodgingException(String message) : base(message) { }
    }
}
