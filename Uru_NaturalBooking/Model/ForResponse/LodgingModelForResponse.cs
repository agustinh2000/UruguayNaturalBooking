using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ForResponse
{
    public class LodgingModelForResponse : ModelBaseForResponse<Lodging, LodgingModelForResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        public List<String> ImagesPath { get; set; }

        public bool IsAvailable { get; set; }

        public double PricePerNight { get; set; }

        public double ReviewsAverageScore { get; set; }

        public List<ReviewModelForResponse> ReviewsForLodging { get; set; }

        public TouristSpotModelForLodgingResponseModel LodgingTouristSpotModel { get; set; }

        public LodgingModelForResponse()
        {
            ReviewsForLodging = new List<ReviewModelForResponse>();
        }

        protected override LodgingModelForResponse SetModel(Lodging lodging)
        {
            Id = lodging.Id;
            Name = lodging.Name;
            Description = lodging.Description;
            QuantityOfStars = lodging.QuantityOfStars;
            Address = lodging.Address;
            ImagesPath = lodging.Images.ConvertAll(p => p.Picture.Path);
            PricePerNight = lodging.PricePerNight;
            ReviewsAverageScore = Math.Round(lodging.ReviewsAverageScore, 2);
            LodgingTouristSpotModel = TouristSpotModelForLodgingResponseModel.ToModel(lodging.TouristSpot);
            ReviewsForLodging = lodging.Reviews.ConvertAll(r => ReviewModelForResponse.ToModel(r));
            IsAvailable = lodging.IsAvailable;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is LodgingModelForResponse response &&
                   Name == response.Name &&
                   Description == response.Description &&
                   QuantityOfStars == response.QuantityOfStars &&
                   Address == response.Address &&
                   ImagesPath.SequenceEqual(response.ImagesPath) &&
                   PricePerNight == response.PricePerNight &&
                   ReviewsAverageScore == response.ReviewsAverageScore &&
                   ReviewsForLodging.SequenceEqual(response.ReviewsForLodging) &&
                   LodgingTouristSpotModel.Equals(response.LodgingTouristSpotModel);
        }
    }
}
