using DomainException;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForRequest
{
    public class SearchOfLodgingModelForRequest
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int QuantityOfAdult { get; set; }

        public int QuantityOfChilds { get; set; }

        public int QuantityOfBabies { get; set; }

        public int QuantityOfRetireds { get; set; }

        public Guid TouristSpotIdSearch { get; set; }

        public void VerifyFormat()
        {
            if (IsInvalidQuantityOfGuest())
            {
                throw new SearchException(MessageExceptionDomain.ErrorQuantityGuestNegative);
            }
            if (HasNotGuest())
            {
                throw new SearchException(MessageExceptionDomain.ErrorQuantityOfGuest);
            }
            if (CheckInIsAfterCheckOut())
            {
                throw new SearchException(MessageExceptionDomain.ErrorDate);
            }
        }

        private bool IsInvalidQuantityOfGuest()
        {
            return QuantityOfAdult < 0 || QuantityOfBabies < 0 || QuantityOfChilds < 0 || QuantityOfRetireds < 0;
        }

        private bool HasNotGuest()
        {
            return QuantityOfAdult == 0 && QuantityOfBabies == 0 && QuantityOfChilds == 0 && QuantityOfRetireds == 0;
        }

        private bool CheckInIsAfterCheckOut()
        {
            return CheckIn > CheckOut;
        }

    }
}
