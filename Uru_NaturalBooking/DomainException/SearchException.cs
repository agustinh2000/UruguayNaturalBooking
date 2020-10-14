using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class SearchException : Exception
    {
        public SearchException(String message) : base(message) { }
    }
}
