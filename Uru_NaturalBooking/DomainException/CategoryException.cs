using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class CategoryException : Exception
    {
        public CategoryException() : base() { }


        public CategoryException(String message) : base(message) { }


        public CategoryException(String message, Exception exception) : base(message, exception) { }
    }
}
