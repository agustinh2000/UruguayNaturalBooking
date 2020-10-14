using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class CategoryException : Exception
    {
        public CategoryException(String message) : base(message) { }
    }
}
