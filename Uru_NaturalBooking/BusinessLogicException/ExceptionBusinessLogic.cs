using System;

namespace BusinessLogicException
{
    public class ExceptionBusinessLogic : Exception
    {
        public ExceptionBusinessLogic() : base() { }

        public ExceptionBusinessLogic(String message) : base(message) { }

        public ExceptionBusinessLogic(String message, Exception exception) : base(message, exception) { }
    }
}
