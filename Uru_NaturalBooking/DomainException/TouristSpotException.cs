using System;

namespace DomainException
{
    public class TouristSpotException : Exception
    {
        public TouristSpotException(String message) : base(message) { }
        
    }
}
