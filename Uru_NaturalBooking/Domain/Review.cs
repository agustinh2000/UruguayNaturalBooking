using DomainException;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public int Score { get; set; }

        public string NameOfWhoComments { get; set; }

        public string LastNameOfWhoComments { get; set; }

        public Guid IdOfReserve { get; set; }

        public virtual Lodging LodgingOfReview { get; set; }

        public void VerifyFormat()
        {
            if (IsInvalidDescriptionOrNameOrLastName())
            {
                throw new ReviewException(MessageExceptionDomain.ErrorIsEmpty);
            }
            if (IsInvalidScore())
            {
                throw new ReviewException(MessageExceptionDomain.ErrorInvalidScore);
            }
        }

        private bool IsInvalidDescriptionOrNameOrLastName()
        {
            return String.IsNullOrEmpty(Description) || String.IsNullOrEmpty(NameOfWhoComments)
                || String.IsNullOrEmpty(LastNameOfWhoComments);
        }

        private bool IsInvalidScore()
        {
            return Score < 1 || Score > 5;
        }

        public override bool Equals(object obj)
        {
            return obj is Review review &&
                Id == review.Id &&
                   Description.Equals(review.Description) &&
                   Score == review.Score &&
                   NameOfWhoComments.Equals(review.NameOfWhoComments) &&
                   LastNameOfWhoComments.Equals(review.LastNameOfWhoComments) &&
                   IdOfReserve == review.IdOfReserve;
        }
    }
}
