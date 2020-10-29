using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForResponse
{
    public class TouristSpotModelForLodgingResponseModel : ModelBaseForResponse<TouristSpot, TouristSpotModelForLodgingResponseModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TouristSpotModelForLodgingResponseModel() { }

        protected override TouristSpotModelForLodgingResponseModel SetModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is TouristSpotModelForLodgingResponseModel model &&
                   Name.Equals(model.Name);
        }
    }
}
