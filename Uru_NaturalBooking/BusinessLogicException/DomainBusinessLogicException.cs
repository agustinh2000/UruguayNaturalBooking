using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicException
{
    public class DomainBusinessLogicException : Exception
    {
        public DomainBusinessLogicException() : base() { }

        public DomainBusinessLogicException(String message) : base(message) { }

    }
}
