using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class LodgingException : Exception
    {
        public LodgingException(String message) : base(message) { }
    }
}
