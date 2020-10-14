using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class ReserveException : Exception
    {
        public ReserveException(String message) : base(message) { }
    }
}
