using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class ReviewException : Exception
    {
        public ReviewException(String message) : base(message) { }

    }
}
