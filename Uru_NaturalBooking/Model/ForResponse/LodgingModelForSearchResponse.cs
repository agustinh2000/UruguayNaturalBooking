using Domain;
using System;
using System.Collections.Generic;

namespace Model.ForResponse
{
    public class LodgingModelForSearchResponse : ModelBaseForResponse<Lodging, LodgingModelForSearchResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        public List<String> ImagesPath { get; set; }

        public double PricePerNight { get; set; }

        public double ReviewsAverageScore { get; set; }

        public TouristSpotModelForLodgingResponseModel LodgingTouristSpotModel { get; set; }

        public LodgingModelForSearchResponse() { }

        protected override LodgingModelForSearchResponse SetModel(Lodging lodging)
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
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is LodgingModelForSearchResponse response &&
                   Id.Equals(response.Id) &&
                   Name == response.Name &&
                   QuantityOfStars == response.QuantityOfStars &&
                   Address == response.Address &&
                   PricePerNight == response.PricePerNight;
        }
    }
}
