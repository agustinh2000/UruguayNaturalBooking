using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class ReserveException : Exception
    {
        public ReserveException() : base() { }


        public ReserveException(String message) : base(message) { }


        public ReserveException(String message, Exception exception) : base(message, exception) { }
    }
}
