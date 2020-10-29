using Domain;
using System;


namespace Model.ForResponse
{
    public class ReviewModelForResponse : ModelBaseForResponse<Review, ReviewModelForResponse>
    {
        public Guid Id { get; set; }
        
        public int Score { get; set; }

        public string Description { get; set; }

        public string NameOfWhoComments { get; set; }

        public string LastNameOfWhoComments { get; set; }


        protected override ReviewModelForResponse SetModel(Review entity)
        {
            Id = entity.Id;
            Score = entity.Score;
            Description = entity.Description;
            NameOfWhoComments = entity.NameOfWhoComments;
            LastNameOfWhoComments = entity.LastNameOfWhoComments;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is ReviewModelForResponse response &&
                   Score == response.Score &&
                   Description.Equals(response.Description) &&
                   NameOfWhoComments.Equals(response.NameOfWhoComments) &&
                   LastNameOfWhoComments.Equals(response.LastNameOfWhoComments);
        }
    }
}
