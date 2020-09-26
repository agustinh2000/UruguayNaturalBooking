using System;

namespace DomainException
{
    public class TouristSpotException : Exception
    {
        public TouristSpotException() : base() { }

        public TouristSpotException(String message) : base(message) { }

        public TouristSpotException(String message, Exception exception) : base(message, exception) { }

        
    }
}
