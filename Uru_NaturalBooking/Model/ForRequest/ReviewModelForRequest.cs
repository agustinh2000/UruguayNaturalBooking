using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForRequest
{
    public class ReviewModelForRequest : ModelBaseForRequest<Review, ReviewModelForRequest>
    {
        public Guid IdOfReserveAssociated { get; set; }

        public int Score { get; set; }

        public string Description { get; set; }

        
        public override Review ToEntity() => new Review()
        {
            Description = Description,
            Score = Score
        };

        public override bool Equals(object obj)
        {
            return obj is ReviewModelForRequest request &&
                   IdOfReserveAssociated.Equals(request.IdOfReserveAssociated) &&
                   Score == request.Score &&
                   Description.Equals(request.Description);
        }
    }
}
